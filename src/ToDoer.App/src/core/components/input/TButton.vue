<template>
  <r-btn
    :variant="variant"
    :type="type"
    :size="size"
    :to="to"
    :pill="pill"
    :disabled="disabled"
  >
    <t-loading-indicator v-if="loading" size="sm" :text="loadingText" />
    <span v-else>
      <slot />
    </span>
  </r-btn>
</template>

<script lang="ts">
import { defineProp } from "../../utils/props";
export default {
  name: "TButton",
  props: {
    to: defineProp([Object, String], false, null),
    loading: defineProp(Boolean, false, false),
    loadingText: defineProp(String, false, null),
    variant: defineProp(String, false, "primary"),
    pill: defineProp(Boolean, false, false),
    disabled: defineProp(Boolean, false, false),
    type: defineProp(String, false, "button"),
    size: defineProp(String, false, "md"),
  },
};
</script>

<style lang="scss" scoped>
</style>
