<template>
  <r-modal
    :id="id"
    ref="taskModal"
    title="Edit task"
    :okButton="okButton"
    :cancelButton="cancelButton"
  >
    <div>
      <t-input
        id="taskNameInput"
        label="Name"
        :model="v$.task.name"
        class="mb-3"
      />

      <t-text-area
        id="taskNoteInput"
        label="Note"
        :model="v$.task.note"
        class="mb-3"
      />

      <t-date-picker
        id="taskDueDateInput"
        label="Due"
        :model="v$.task.dueDate"
      />
    </div>
  </r-modal>
</template>

<script lang="ts">
import { defineProp, idProp } from "../../core/utils/props";
import { useVuelidate } from "@vuelidate/core";
import { required } from "@vuelidate/validators";
import { IModalButton } from "razor-wire";
import { isUndefinedOrNull } from "../../core/utils/inspect";

export default {
  setup() {
    return { v$: useVuelidate() };
  },
  name: "TaskModal",
  props: {
    ...idProp,
    editTask: defineProp(Object, false, null),
  },
  data() {
    return {
      task: {
        name: "",
        note: "",
        dueDate: null,
      },
      okButton: {
        label: "Save",
        variant: "primary",
        onClick: this.save,
      } as IModalButton,
      cancelButton: {
        label: "Cancel",
        variant: "secondary",
        onClick: this.cancel,
      } as IModalButton,
    };
  },
  validations() {
    return {
      task: {
        name: {
          required,
        },
        note: {},
        dueDate: {},
      },
    };
  },
  methods: {
    save(hideModal: () => void) {
      this.v$.$touch();
      if (!this.v$.task.$invalid) {
        this.$emit("save", { ...this.task, hideModal });
        this.resetModal();
      }
    },
    cancel() {
      this.resetModal();
      this.$emit("cancel");
    },
    resetModal() {
      this.task.name = "";
      this.task.note = "";
      this.task.dueDate = null;
      this.v$.$reset();
    },
  },
  watch: {
    editTask: {
      handler() {
        if (!isUndefinedOrNull(this.editTask)) {
          this.task = { ...this.editTask };
        }
      },
      deep: true,
    },
  },
};
</script>

<style lang="scss" scoped>
</style>
