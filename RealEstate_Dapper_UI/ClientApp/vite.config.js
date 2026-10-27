// Vite only compiles static assets into the UI project's wwwroot/dist.
// ASP.NET Core keeps hosting, routing and server rendering (Razor MVC).
import { defineConfig } from 'vite';
import react from '@vitejs/plugin-react';
import { fileURLToPath } from 'node:url';

const outDir = fileURLToPath(new URL('../wwwroot/dist', import.meta.url));

export default defineConfig(({ mode }) => ({
  base: '/dist/',
  plugins: [react()],
  publicDir: false,
  build: {
    outDir,
    emptyOutDir: true,
    manifest: false,
    sourcemap: mode === 'development',
    cssCodeSplit: true,
    target: 'es2020',
    assetsInlineLimit: 0,
    rollupOptions: {
      input: {
        site: fileURLToPath(new URL('./src/site/main.js', import.meta.url)),
        dashboard: fileURLToPath(new URL('./src/dashboard/main.js', import.meta.url)),
        'site-styles': fileURLToPath(new URL('./src/styles/site.entry.css', import.meta.url)),
        'dashboard-styles': fileURLToPath(new URL('./src/styles/dashboard.entry.css', import.meta.url)),
      },
      output: {
        // Stable entry names; Razor adds ?v=<hash> through asp-append-version.
        entryFileNames: '[name].js',
        chunkFileNames: 'chunks/[name]-[hash].js',
        assetFileNames: (info) => {
          const name = info.names?.[0] ?? info.name ?? '';
          // CSS entries 'site-styles' / 'dashboard-styles' -> site.css / dashboard.css
          if (name.endsWith('.css')) return name.replace('-styles', '');
          return 'assets/[name]-[hash][extname]';
        },
      },
    },
  },
}));
