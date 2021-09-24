<template>
  <div
    :class="{
      'd-block': show,
      'd-none': !show,
      'valid-feedback': state === true,
      'invalid-feedback': state === false,
    }"
  >
    <slot />
  </div>
</template>

<script lang="ts">
import { defineProp } from "../../utils/props";
import { isUndefinedOrNull } from "../../utils/inspect";

export default {
  name: "TInputFeedback",
  props: {
    state: defineProp(Boolean, false, false),
  },
  computed: {
    show() {
      return !isUndefinedOrNull(this.state);
    },
  },
};
</script>

<style lang="scss" scoped>
</style>
