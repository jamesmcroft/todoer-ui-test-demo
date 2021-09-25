<template>
  <div class="d-flex flex-column align-items-stretch bg-white">
    <div
      class="d-flex align-items-center p-3 text-decoration-none border-bottom"
    >
      <span class="fs-5 fw-semibold">Task lists</span>
      <t-button
        id="addTaskListButton"
        class="ms-auto"
        :icon="['fas', 'plus']"
        v-r-toggle.addEditUpdateTaskListModal
        @click="onAddTaskList"
      />
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
          <t-button
            class="task-list-item-edit ms-auto me-2"
            variant="warning"
            :icon="['fas', 'edit']"
            v-r-toggle.addEditUpdateTaskListModal
            @click.stop.prevent="onEditTaskList(taskList)"
          />
          <t-button
            class="task-list-item-delete"
            variant="danger"
            :icon="['fas', 'trash']"
            @click.stop.prevent="onDeleteTaskList(taskList)"
          />
        </div>
      </button>
    </div>

    <task-list-modal
      id="addEditUpdateTaskListModal"
      @save="onAddEditUpdateTaskList"
      :editTaskList="editTaskList"
    />
  </div>
</template>

<script lang="ts">
import TButton from "../../core/components/input/TButton.vue";
import { showErrorMessage } from "../../core/components/messaging/ToastHelper";
import { isUndefinedOrNull } from "../../core/utils/inspect";
import { defineProp, idProp } from "../../core/utils/props";
import ITaskListSummaryViewModel from "../../services/tasks/models/ITaskListSummaryViewModel";
import TaskListDetailViewModel from "../../services/tasks/models/TaskListDetailViewModel";
import tasksService from "../../services/tasks/TasksService";
import TaskListModal from "./TaskListModal.vue";

export default {
  components: { TButton, TaskListModal },
  name: "TaskListSidebar",
  props: {
    ...idProp,
    taskLists: defineProp(Array, true),
    selectedTaskList: defineProp(Object, true),
  },
  data() {
    return {
      editTaskList: new TaskListDetailViewModel(),
    };
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
    onAddTaskList() {
      this.editTaskList = new TaskListDetailViewModel();
      this.$emit("selected", null);
    },
    async onAddEditUpdateTaskList(addUpdateTaskList: {
      id: string;
      name: string;
      hideModal: () => void;
    }) {
      var result = addUpdateTaskList.id
        ? await tasksService.updateTaskList(addUpdateTaskList.id, {
            name: addUpdateTaskList.name,
          })
        : await tasksService.addTaskList({
            name: addUpdateTaskList.name,
          });

      if (!result.isSuccessStatusCode) {
        showErrorMessage(this, "Couldn't update task lists");
      } else {
        addUpdateTaskList.hideModal();
        this.editTaskList = new TaskListDetailViewModel();
        this.$emit("updated");
        if (addUpdateTaskList.id) {
          this.selectTaskList(addUpdateTaskList);
        }
      }
    },
    onEditTaskList(taskList: ITaskListSummaryViewModel) {
      this.editTaskList = taskList;
    },
    async onDeleteTaskList(taskList: ITaskListSummaryViewModel) {
      var result = await tasksService.deleteTaskList(taskList.id);
      if (!result.isSuccessStatusCode) {
        showErrorMessage(this, "Couldn't delete task list");
      } else {
        if (taskList.id === this.selectedTaskList.id) {
          this.$emit("selected", null);
        }
        this.$emit("updated");
      }
    },
  },
};
</script>

<style lang="scss" scoped>
</style>
