<template>
  <div
    :class="{
      'bg-primary': variant === 'primary',
      'bg-danger': variant === 'danger',
      'bg-info': variant === 'info',
      'bg-warning': variant === 'warning',
      'bg-success': variant === 'success',
    }"
    class="toast align-items-center text-white bg-primary border-0 showing"
    role="alert"
    aria-live="assertive"
    aria-atomic="true"
    data-bs-animation="true"
  >
    <div class="d-flex">
      <div class="toast-body">{{ message }}</div>
      <button
        type="button"
        class="btn-close btn-close-white me-2 m-auto"
        data-bs-dismiss="toast"
        aria-label="Close"
        @click="onDismiss"
      ></button>
    </div>
  </div>
</template>

<script lang="ts">
import { PropType } from "@vue/runtime-core";
import { Timer } from "made-threading";
import IToastData from "./TToastData";
import { defineProp } from "../../utils/props";

export default {
  name: "TToast",
  props: {
    toast: defineProp(Object as PropType<IToastData>, true),
  },
  computed: {
    variant() {
      return this.toast.variant;
    },
    message() {
      return this.toast.message;
    },
    duration() {
      return this.toast.duration;
    },
  },
  methods: {
    onDismiss() {
      this.$emit("close", this.toast);
    },
    configureTimeout() {
      if (this.duration) {
        this.timer = new Timer(this.onDismiss, this.duration, false);
        this.timer.start();
      }
    },
  },
  mounted() {
    this.configureTimeout();
  },
};
</script>
