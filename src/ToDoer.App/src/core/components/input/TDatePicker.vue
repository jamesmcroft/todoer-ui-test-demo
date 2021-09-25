<template>
  <div>
    <r-label v-if="label" :label-for="id">{{ label }}</r-label>

    <div class="input-group">
      <span class="input-group-text" v-if="text">{{ text }}</span>

      <v-date-picker
        :id="id"
        :ref="id"
        v-model="model.$model"
        :state="validationState"
      />

      <t-input-feedback :state="validationState">
        <div v-if="model && model.required && model.required.$invalid">
          {{ $t("validationMessages.required") }}
        </div>
      </t-input-feedback>
    </div>
  </div>
</template>

<script lang="ts">
import { defineProp, idProp } from "../../utils/props";
import { isNullOrWhiteSpace } from "made-data-validation";

export default {
  name: "TDatePicker",
  props: {
    ...idProp,
    label: defineProp(String, false, null),
    text: defineProp(String, false, null),
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
