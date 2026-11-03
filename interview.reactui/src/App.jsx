import { useEffect, useMemo, useState } from 'react'
import './App.css'

const apiBaseUrl = import.meta.env.VITE_API_BASE_URL || '/api'

function App() {
  const [products, setProducts] = useState([])
  const [categories, setCategories] = useState([])
  const [search, setSearch] = useState('')
  const [appliedSearch, setAppliedSearch] = useState('')
  const [categoryId, setCategoryId] = useState('')
  const [sort, setSort] = useState('name')
  const [direction, setDirection] = useState('asc')
  const [loading, setLoading] = useState(true)
  const [error, setError] = useState('')

  useEffect(() => {
    const controller = new AbortController()
    async function loadData() {
      setLoading(true)
      setError('')
      try {
        const query = appliedSearch.trim() ? `?search=${encodeURIComponent(appliedSearch.trim())}` : ''
        const [productsResponse, categoriesResponse] = await Promise.all([
          fetch(`${apiBaseUrl}/products${query}`, { signal: controller.signal }),
          fetch(`${apiBaseUrl}/categories`, { signal: controller.signal }),
        ])
        if (!productsResponse.ok || !categoriesResponse.ok) throw new Error('API request failed')
        const [productData, categoryData] = await Promise.all([productsResponse.json(), categoriesResponse.json()])
        setProducts(productData)
        setCategories(categoryData.filter((category) => category.isActive))
      } catch (requestError) {
        if (requestError.name !== 'AbortError') setError('Unable to load products. Make sure the Interview API is running.')
      } finally {
        if (!controller.signal.aborted) setLoading(false)
      }
    }
    loadData()
    return () => controller.abort()
  }, [appliedSearch])

  const visibleProducts = useMemo(() => {
    const categoryNames = new Map(categories.map((category) => [category.categoryId, category.name]))
    const filtered = products.filter((product) => product.isActive && (!categoryId || product.categoryId === categoryId)).map((product) => ({
      ...product,
      categoryName: categoryNames.get(product.categoryId) || 'Uncategorized',
    }))
    return filtered.sort((left, right) => {
      const leftValue = sort === 'category' ? left.categoryName : left[sort]
      const rightValue = sort === 'category' ? right.categoryName : right[sort]
      const result = String(leftValue || '').localeCompare(String(rightValue || ''))
      return direction === 'asc' ? result : -result
    })
  }, [categories, categoryId, direction, products, sort])

  function changeSort(column) {
    if (sort === column) setDirection((current) => current === 'asc' ? 'desc' : 'asc')
    else { setSort(column); setDirection('asc') }
  }

  function clearFilters() {
    setSearch(''); setAppliedSearch(''); setCategoryId(''); setSort('name'); setDirection('asc')
  }

  return (
    <>
      <header><nav className="navbar"><div className="navbar-content">
        <a className="brand" href="#products">React UI</a>
        <button className="menu-button" type="button" aria-label="Toggle navigation">☰</button>
        <ul className="navigation"><li><a href="#home">Home</a></li><li><a className="active" href="#products">Products</a></li><li><a href="#privacy">Privacy</a></li></ul>
      </div></nav></header>
      <main className="container" id="products">
        <h1>Products</h1>
        <form className="filter-form" onSubmit={(event) => { event.preventDefault(); setAppliedSearch(search) }}>
          <input className="form-control search-input" value={search} onChange={(event) => setSearch(event.target.value)} placeholder="Search by name or SKU" aria-label="Search by name or SKU" type="search" />
                  <select className="form-control category-select"
                      value={categoryId} onChange={(event) => setCategoryId(event.target.value)}
                  >
                      <option value="">All Categories</option>
                      {categories.map((category) => <option key={category.categoryId}
                          value={category.categoryId}
                      >{category.name}</option>)}
                  </select>
          <button className="button secondary" type="button" onClick={clearFilters}>Clear</button> 
        </form>
        {error && <div className="alert" role="alert">{error}</div>}
        <div className="table-wrapper"><table className="products-table"><thead><tr>{['category', 'name', 'sku'].map((column) => <th key={column}><button type="button" onClick={() => changeSort(column)}>{column[0].toUpperCase() + column.slice(1)}{sort === column ? (direction === 'asc' ? ' ▲' : ' ▼') : ''}</button></th>)}</tr></thead>
          <tbody>{loading ? <tr><td className="message" colSpan="3">Loading products...</td></tr> : visibleProducts.length === 0 ? <tr><td className="message" colSpan="3">No products found.</td></tr> : visibleProducts.map((product) => <tr key={product.id}><td>{product.categoryName}</td><td>{product.name}</td><td>{product.sku}</td></tr>)}</tbody>
        </table></div>
      </main>
      <footer><div className="container">© 2026 - Interview.Web - <a href="#privacy">Privacy</a></div></footer>
    </>
  )
}

export default App
