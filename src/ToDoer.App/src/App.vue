<template>
  <app-layout>
    <router-view />

    <t-toast-panel
      id="appToasts"
      ref="appToasts"
      :toasts="appToasts"
      @remove="onRemoveToast"
    />
  </app-layout>
</template>

<script lang="ts">
import { defineComponent } from "vue";
import IToastData from "./core/components/messaging/TToastData";
import Emitter from "./core/messaging/Emitter";

export default defineComponent({
  data() {
    return {
      appToasts: [] as Array<IToastData>,
    };
  },
  async mounted() {
    Emitter.$on("show-toast", (toast: IToastData) => {
      this.appToasts.push(toast);
    });
  },
  methods: {
    onRemoveToast(toast: { id: string }) {
      this.appToasts = this.appToasts.filter(
        (t: { id: string }) => t.id !== toast.id
      );
    },
  },
});
</script>

<style lang="scss"></style>
