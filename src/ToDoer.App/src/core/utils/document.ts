export const clickElement = (selector: string) => {
  var element = document.querySelector(selector) as HTMLElement;
  if (element) {
    element.click();
  }
}
