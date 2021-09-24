import { App } from "vue";
import moment, { MomentInput } from "moment-timezone";

const timezone = moment.tz.guess();

const DateTimeFormat = {
  install(vueApp: App) {
    vueApp.config.globalProperties.$dateFilters = {
      taskDate(value: MomentInput) {
        if (value) {
          return moment.utc(value).tz(timezone).format("ddd, MMM d");
        }
      },

    };
  },
};

export default DateTimeFormat;
