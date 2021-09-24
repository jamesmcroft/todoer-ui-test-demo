import { defineConfig } from "vite";
import vue from "@vitejs/plugin-vue";
import mkcert from 'vite-plugin-mkcert'
import Pages from "vite-plugin-pages";
import path from "path";

export default defineConfig(({ mode }) => {
  require("dotenv").config({ path: `./.env.${mode}` });

  return {
    css: {
      preprocessorOptions: {
        scss: {
        },
      },
    },
    define: {
      "process.env": { ...process.env, APP_MODE: mode },
    },
    server: {
      https: true,
      port: (mode === "development" ? 3000 : null)
    },
    plugins: [
      mkcert(),
      vue(),
      Pages({
        pagesDir: "src/pages",
      }),
    ],
    resolve: {
      alias: {
        "@": path.resolve(__dirname, "/src"),
      },
    },
  };
});
