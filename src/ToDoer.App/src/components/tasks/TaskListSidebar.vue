<template>
  <div class="d-flex flex-column align-items-stretch bg-white">
    <div
      class="d-flex align-items-center p-3 text-decoration-none border-bottom"
    >
      <span class="fs-5 fw-semibold">Task lists</span>
    </div>
    <div
      class="
        task-list-group
        list-group list-group-flush
        border-bottom
        scrollarea
      "
    >
      <button
        v-for="taskList in taskLists"
        :key="taskList.id"
        :class="{ active: isSelectedTaskList(taskList.id) }"
        class="
          task-list-item
          list-group-item list-group-item-action
          py-3
          lh-tight
        "
        aria-current="true"
        @click="selectTaskList(taskList)"
      >
        <div class="d-flex w-100 align-items-center justify-content-between">
          <strong class="task-list-item-title mb-1">{{ taskList.name }}</strong>
        </div>
      </button>
    </div>
  </div>
</template>

<script lang="ts">
import TButton from "../../core/components/input/TButton.vue";
import { isUndefinedOrNull } from "../../core/utils/inspect";
import { defineProp, idProp } from "../../core/utils/props";
import ITaskListSummaryViewModel from "../../services/tasks/models/ITaskListSummaryViewModel";

export default {
  components: { TButton },
  name: "TaskListSidebar",
  props: {
    ...idProp,
    taskLists: defineProp(Array, true),
    selectedTaskList: defineProp(Object, true),
  },
  methods: {
    isSelectedTaskList(taskListId: number) {
      if (!isUndefinedOrNull(this.selectedTaskList)) {
        return this.selectedTaskList.id === taskListId;
      }
      return false;
    },
    selectTaskList(taskList: ITaskListSummaryViewModel) {
      this.$emit("selected", taskList);
    },
  },
};
</script>

<style lang="scss" scoped>
</style>
