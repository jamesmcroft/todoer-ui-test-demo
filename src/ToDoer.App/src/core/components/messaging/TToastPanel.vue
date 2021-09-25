<template>
  <div class="toast-container position-absolute top-0 end-0 p-3">
    <t-toast
      v-for="toast in toasts"
      :key="toast.id"
      :toast="toast"
      @close="onRemoveToast"
    />
  </div>
</template>

<script lang="ts">
import { defineProp, idProp } from "../../utils/props";

export default {
  name: "TToastPanel",
  props: {
    ...idProp,
    toasts: defineProp(Array, true),
  },
  methods: {
    onRemoveToast(toast: { id: string }) {
      this.$emit("remove", toast);
    },
  },
};
</script>
