import { createApp } from "vue";
import App from "./App.vue";

import router from "./router";
import i18n from "./core/localisation/i18n";
import axios from "axios";
import VueAxios from "vue-axios";
import RazorWires from "razor-wire";
import "./assets/scss/app.scss";
import store from "./store";
import "bootstrap";
import DateTimeFormat from "./core/styling/DateTimeFormat";
import CoreComponents from "./core/components/CoreComponents";

const app = createApp(App);

DateTimeFormat.install(app);

app.use(VueAxios, axios);
app.use(CoreComponents);
app.use(RazorWires);
app.use(router);

app.use(i18n);
app.use(store);

app.mount("#app");
