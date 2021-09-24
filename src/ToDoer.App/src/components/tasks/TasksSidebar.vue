<template>
  <div class="d-flex flex-column align-items-stretch bg-white">
    <div
      class="d-flex align-items-center p-3 text-decoration-none border-bottom"
    >
      <span class="fs-5 fw-semibold">{{ selectedTaskList.name }}</span>
    </div>
    <div class="list-group list-group-flush border-bottom scrollarea">
      <button
        v-for="task in tasks"
        :key="task.id"
        class="list-group-item list-group-item-action py-3 lh-tight"
        aria-current="true"
      >
        <div class="d-flex w-100 align-items-center justify-content-between">
          <strong class="mb-1">{{ task.name }}</strong>
          <t-check-box />
        </div>
        <div class="col-10 mb-1 small">
          Due {{ $dateFilters.taskDate(task.dueDate) }}
        </div>
      </button>
    </div>
  </div>
</template>

<script lang="ts">
import { isUndefinedOrNull } from "../../core/utils/inspect";
import { defineProp, idProp } from "../../core/utils/props";

export default {
  name: "TasksSidebar",
  props: {
    ...idProp,
    selectedTaskList: defineProp(Object, true),
  },
  computed: {
    tasks() {
      return !isUndefinedOrNull(this.selectedTaskList)
        ? this.selectedTaskList.tasks
        : [];
    },
  },
  methods: {},
};
</script>

<style lang="scss" scoped>
</style>
