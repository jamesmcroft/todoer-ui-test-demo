/**
* Defines a set of easy access application-specific environment variables.
*/
export default {
  IsDevelopment: process.env.APP_MODE === 'development',
  /**
   * Retrieves the base application URL from the environment variables, e.g. https://localhost:3000,
   * used primarily for the AuthService redirect URIs.
   */
  BaseUrl: process.env.BASE_APP_URL,

  /**
   * Retrieves the default locale from the environment variables, e.g. en,
   * used for the i18n default localised strings.
   */
  DefaultLocale: process.env.DEFAULT_LOCALE,
}
