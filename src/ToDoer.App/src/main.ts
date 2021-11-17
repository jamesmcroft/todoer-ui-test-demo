import { createApp } from "vue";
import App from "./App.vue";

import router from "./router";
import i18n from "./core/localisation/i18n";
import axios from "axios";
import VueAxios from "vue-axios";
import RazorWires from "razor-wire";
import Blades from "made-vue-blades";
import Image from "made-vue-image";
import RangeSelector from "made-vue-range-selector";

import "made-vue-blades/styles.scss";
import "made-vue-image/styles.scss";
import "made-vue-range-selector/styles.scss";
import "./assets/scss/app.scss";

import store from "./store";
import "bootstrap";
import FontAwesomeIcons from "./core/styling/FontAwesomeIcons";
import DateTimeFormat from "./core/styling/DateTimeFormat";
import CoreComponents from "./core/components/CoreComponents";
import GlobalToast from "./core/components/messaging/GlobalToast";
import VCalendar from 'v-calendar';

const app = createApp(App);

FontAwesomeIcons.install(app);
DateTimeFormat.install(app);
GlobalToast.install(app);

app.use(VueAxios, axios);
app.use(CoreComponents);
app.use(RazorWires);
app.use(Blades);
app.use(Image);
app.use(RangeSelector);
app.use(VCalendar, {});
app.use(router);

app.use(i18n);
app.use(store);

app.mount("#app");
