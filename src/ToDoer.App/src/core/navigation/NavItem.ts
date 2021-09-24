import { RouteLocation } from "vue-router";

export default interface INavItem {
  text: string | null;
  to: string | RouteLocation | null;
  disabled: boolean;
  visible: boolean;
  active: boolean;
}
