<template>
  <div>
    <r-label v-if="label" :label-for="id">{{ label }}</r-label>

    <div class="input-group">
      <r-check
        :class="{ 'checkbox-lg': size === 'lg' }"
        :id="id"
        :ref="id"
        v-model="model.$model"
        :disabled="disabled"
      />
    </div>
  </div>
</template>

<script lang="ts">
import { defineProp, idProp } from '../../utils/props';

export default {
  name: "TCheckBox",
  props: {
    ...idProp,
    label: defineProp(String, false, null),
    disabled: defineProp(Boolean, false, false),
    model: defineProp(Object, false, {}),
    size: defineProp(String, false, "md"),
  },
};
</script>

<style lang="scss" scoped>
</style>
