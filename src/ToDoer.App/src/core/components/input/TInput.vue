<template>
  <div>
    <r-label v-if="label" :label-for="id">{{ label }}</r-label>

    <div class="input-group">
      <span class="input-group-text" v-if="text">{{ text }}</span>
      <r-input
        :id="id"
        :ref="id"
        :type="type"
        v-model="model.$model"
        :state="validationState"
        :disabled="disabled"
        :placeholder="disabled ? disabledPlaceholder : placeholder"
      />

      <t-input-feedback :state="validationState">
        <div v-if="model && model.required && model.required.$invalid">
          {{ $t("validationMessages.required") }}
        </div>

        <div v-if="model && model.email && model.email.$invalid">
          {{ $t("validationMessages.invalidEmail") }}
        </div>
      </t-input-feedback>
    </div>
  </div>
</template>

<script lang="ts">
import { defineProp, idProp } from "../../utils/props";
import { isNullOrWhiteSpace } from "made-data-validation";

export default {
  name: "TInput",
  props: {
    ...idProp,
    label: defineProp(String, false, null),
    text: defineProp(String, false, null),
    placeholder: defineProp(String, false, ""),
    disabledPlaceholder: defineProp(String, false, "No value provided"),
    disabled: defineProp(Boolean, false, false),
    type: defineProp(String, false, "text"),
    model: defineProp(Object, false, {}),
    indicateWhenValid: defineProp(Boolean, false, false),
  },
  computed: {
    validationState() {
      if (this.model && !this.model.$error) {
        return this.indicateWhenValid ? true : null;
      }
      return false;
    },
  },
  watch: {
    model: {
      handler() {
        if (isNullOrWhiteSpace(this.model.$model)) {
          this.$refs[this.id].$data.value = "";
        } else if (this.$refs[this.id].$data.value !== this.model.$model) {
          this.$refs[this.id].$data.value = this.model.$model;
        }
      },
      deep: true,
    },
  },
};
</script>

<style lang="scss" scoped>
</style>
