import INavItem from "./NavItem";

/**
 * Defines a menu item model.
 */
export default interface IMenuItem extends INavItem {
  icon: string | string[] | null;
  href: string | null;
  children: IMenuItem[] | null;
}
