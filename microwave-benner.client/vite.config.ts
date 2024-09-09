import { fileURLToPath, URL } from 'node:url';
import { defineConfig } from 'vite';
import react from '@vitejs/plugin-react';
import { env } from 'process';

// Defina a URL do backend
const target = env.ASPNETCORE_URLS ? env.ASPNETCORE_URLS.split(';')[0] : 'http://localhost:7141';

// https://vitejs.dev/config/
export default defineConfig({
    plugins: [react()],
    resolve: {
        alias: {
            '@': fileURLToPath(new URL('./src', import.meta.url))
        }
    },
    server: {
        proxy: {
            '^/microwave': {
                target,
                changeOrigin: true,
                secure: false, // Não necessário se não usar HTTPS
            }
        },
        port: 5173,
        // Removido HTTPS
    }
});
