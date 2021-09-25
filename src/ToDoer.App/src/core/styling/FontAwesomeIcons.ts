import { App } from "vue";
import { library } from '@fortawesome/fontawesome-svg-core'
import { faEdit, faTrash, faSave, faPlus } from '@fortawesome/free-solid-svg-icons'
import { FontAwesomeIcon } from '@fortawesome/vue-fontawesome'

const FontAwesomeIcons = {
  install(vueApp: App) {
    library.add(
      faEdit,
      faTrash,
      faSave,
      faPlus
    );

    vueApp.component("font-awesome-icon", FontAwesomeIcon);
  },
};

export default FontAwesomeIcons;
