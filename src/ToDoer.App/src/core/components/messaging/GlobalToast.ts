import { Guid } from 'made-runtime';
import { App } from 'vue';
import Emitter from '../../messaging/Emitter';
import IToastData from './TToastData';

const GlobalToast = {
  install(vueApp: App) {
    vueApp.config.globalProperties.$toast = {
      show(toast: IToastData) {
        Emitter.$emit('show-toast', { ...toast, id: Guid.newGuid().toString() });
      }
    }
  }
};

export default GlobalToast;
