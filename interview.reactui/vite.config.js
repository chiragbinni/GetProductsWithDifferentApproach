import { defineConfig } from 'vite';
import plugin from '@vitejs/plugin-react';

export default defineConfig({
    plugins: [plugin()],
    server: {
        port: 58730,
        proxy: {
            '/api': {
                target: 'https://localhost:7072',
                secure: false,
            },
        },
    },
})