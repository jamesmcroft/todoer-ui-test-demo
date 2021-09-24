<template>
  <div
    class="loading-indicator d-flex flex-column justify-content-around"
    :class="{
      sm: size === 'sm',
      'loading-indicator-dark': variant === 'dark',
      'loading-indicator-light': variant === 'light',
      'loading-indicator-primary': variant === 'primary',
    }"
  >
    <div class="d-flex justify-content-around">
      <div class="loading-content"></div>
    </div>
    <div class="d-flex justify-content-around">
      <div class="loading-text" v-if="text">
        <span>{{ text }}</span>
      </div>
    </div>
  </div>
</template>

<script lang="ts">
import { defineProp } from "../../utils/props";

export default {
  name: "TLoadingIndicator",
  props: {
    text: defineProp(String, false, null),
    size: defineProp(String, false, ""),
    variant: defineProp(String, false, "primary"),
  },
};
</script>

<style lang="scss" scoped>
</style>
