<template>
  <div class="container-fluid min-vh-100 d-flex flex-column">
    <slot />
  </div>
</template>

<script lang="ts">
import { defineComponent } from "vue";
import Emitter from "../../messaging/Emitter";

export default defineComponent({
  name: "AppLayout",
  mounted() {
    Emitter.$on("logged-out", this.handleUserLoggedOut);
  },
  beforeUnmount() {
    Emitter.$off("logged-out");
  },
  methods: {
    handleUserLoggedOut() {
      if (this.$router.currentRoute.name === "login") {
        return;
      }

      this.$router.push({ name: "login" });
    },
  },
});
</script>

<style lang="scss" scoped></style>
