<template>
  <button
    class="task-item list-group-item list-group-item-action py-3 lh-tight"
    aria-current="true"
  >
    <div class="d-flex w-100 align-items-center justify-content-between">
      <strong class="task-item-title mb-1">{{ task.name }}</strong>
      <r-check
        class="task-item-complete checkbox-lg ms-auto me-3"
        v-model="task.completed"
        @change="onCompleteTask(task)"
      />

      <t-button
        class="task-item-delete"
        variant="danger"
        @click="onDeleteTask(task)"
        >Delete</t-button
      >
    </div>
    <div class="task-item-due col-10 mb-1 small" v-if="task.dueDate">
      Due {{ $dateFilters.taskDate(task.dueDate) }}
    </div>
  </button>
</template>

<script lang="ts">
import { defineProp } from "../../core/utils/props";
import ITaskDetailViewModel from "../../services/tasks/models/ITaskDetailViewModel";

export default {
  name: "TaskItem",
  props: {
    task: defineProp(Object, true),
  },
  methods: {
    onCompleteTask(task: ITaskDetailViewModel) {
      this.$emit("update", task);
    },
    onDeleteTask(task: ITaskDetailViewModel) {
      this.$emit("delete", task);
    },
  },
};
</script>

<style lang="scss" scoped>
</style>
