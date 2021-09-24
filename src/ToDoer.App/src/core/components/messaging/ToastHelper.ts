import IToastData from "./TToastData";

/**
* Displays a toast notification based on an error message.
* @param instance The instance of the component that called this function.
* @param message The error message that should be displayed.
*/
export function showErrorMessage(instance: any, message: string) {
  if (!instance.$toast) {
    return;
  }

  instance.$toast
    .show({
      variant: 'error',
      message: message,
      duration: 3000
    } as IToastData);
}
