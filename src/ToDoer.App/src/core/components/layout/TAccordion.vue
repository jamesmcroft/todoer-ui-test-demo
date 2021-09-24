<template>
  <div class="accordion" :id="id">
    <div class="accordion-item">
      <h2 class="accordion-header" :id="headerId">
        <button
          class="accordion-button"
          type="button"
          data-bs-toggle="collapse"
          :data-bs-target="contentTarget"
          :aria-expanded="showExpanded"
          :aria-controls="contentId"
        >
          <slot name="header"> {{ header }} </slot>
        </button>
      </h2>

      <div
        :id="contentId"
        class="accordion-collapse collapse show"
        :aria-labelledby="headerId"
        :data-bs-parent="idTarget"
      >
        <div class="accordion-body">
          <slot> </slot>
        </div>
      </div>
    </div>
  </div>
</template>

<script lang="ts">
import { defineComponent } from "vue";
import { defineProp, idProp } from "../../utils/props";

export default defineComponent({
  name: "TAccordion",
  props: {
    ...idProp,
    header: defineProp(String, false, ""),
    showExpanded: defineProp(Boolean, false, false),
  },
  computed: {
    idTarget(): String {
      return `#${this.id}`;
    },
    headerId(): String {
      return `${this.id}-header`;
    },
    contentId(): String {
      return `${this.id}-content`;
    },
    contentTarget(): String {
      return `#${this.contentId}`;
    },
  },
});
</script>

<style lang="scss" scoped>
</style>
