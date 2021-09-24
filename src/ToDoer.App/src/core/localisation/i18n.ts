import { createI18n } from 'vue-i18n'
import environment from '../utils/environment';
import localLoader from './locale.loader';

function loadLocaleInfo() {
  const locales = localLoader;
  const messages = {};
  Object.keys(locales).forEach((key) => {
    messages[key] = locales[key];
  });
  return { messages };
}

const { messages } = loadLocaleInfo();

const dateTimeFormats = {
  'en-GB': {
    time: {
      hour: 'numeric',
      minute: 'numeric'
    },
    short: {
      year: 'numeric',
      month: '2-digit',
      day: '2-digit'
    }
  }
};

const i18n = createI18n({
  locale: environment.DefaultLocale,
  dateTimeFormats,
  messages,
  silentTranslationWarn: true
});

export default i18n;
