<template>
  <main>
    <form>
      <img class="mb-4" />
      <h1 class="h3 mb-3 fw-normal">Login</h1>

      <t-input
        class="mb-2"
        type="email"
        id="emailInput"
        :model="v$.login.email"
        label="Email"
        placeholder="email@example.co.uk"
      />

      <t-input
        class="mb-2"
        type="password"
        label="Password"
        id="passwordInput"
        :model="v$.login.password"
        placeholder="Password"
      />

      <t-button id="loginButton" class="w-100" @click="signIn"
        >Sign in</t-button
      >
    </form>
  </main>
</template>

<script lang="ts">
import { defineComponent } from "@vue/runtime-core";
import { useVuelidate } from "@vuelidate/core";
import { required, email } from "@vuelidate/validators";
import { showErrorMessage } from "../../core/components/messaging/ToastHelper";

import authenticationService from "../../services/authentication/AuthenticationService";

export default defineComponent({
  setup() {
    return { v$: useVuelidate() };
  },
  name: "Login",
  data() {
    return {
      login: {
        email: "",
        password: "",
      },
    };
  },
  validations() {
    return {
      login: {
        email: {
          required,
          email,
        },
        password: {
          required,
        },
      },
    };
  },
  methods: {
    async signIn(evt: { preventDefault: () => void }) {
      this.v$.$touch();
      if (!this.v$.login.$invalid) {
        evt?.preventDefault();

        var result = await authenticationService.login(
          this.login.email,
          this.login.password
        );

        if (!result.isSuccessStatusCode()) {
          showErrorMessage(this, "Login failed");
          return;
        } else {
          this.$router.push({ name: "index" });
        }
      }
    },
  },
});
</script>

<style lang="scss" scoped>
</style>

<route lang="yml">
meta:
  allowAnonymous: true
  title: "Login"
</route>
