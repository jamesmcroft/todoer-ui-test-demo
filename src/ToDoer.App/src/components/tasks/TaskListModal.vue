<template>
  <r-modal
    :id="id"
    ref="taskListModal"
    :title="title"
    :okButton="okButton"
    :cancelButton="cancelButton"
  >
    <div>
      <t-input id="taskListNameInput" label="Name" :model="v$.taskList.name" />
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
  name: "TaskListModal",
  props: {
    ...idProp,
    editTaskList: defineProp(Object, false, null),
  },
  computed: {
    title() {
      return !isUndefinedOrNull(this.editTaskList?.id)
        ? "Edit Task List"
        : "New Task List";
    },
  },
  data() {
    return {
      taskList: {
        name: "",
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
      taskList: {
        name: {
          required,
        },
      },
    };
  },
  methods: {
    save(hideModal: () => void) {
      this.v$.$touch();
      if (!this.v$.taskList.$invalid) {
        this.$emit("save", { ...this.taskList, hideModal });
        this.resetModal();
      }
    },
    cancel() {
      this.resetModal();
      this.$emit("cancel");
    },
    resetModal() {
      this.taskList.name = "";
      this.v$.$reset();
    },
  },
  watch: {
    editTaskList: {
      handler() {
        if (!isUndefinedOrNull(this.editTaskList)) {
          this.taskList = { ...this.editTaskList };
        }
      },
      deep: true,
    },
  },
};
</script>

<style lang="scss" scoped>
</style>
