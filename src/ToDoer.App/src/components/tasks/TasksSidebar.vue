<template>
  <div :id="id" class="d-flex flex-column align-items-stretch bg-white">
    <div
      class="
        d-flex
        align-items-center
        p-3
        text-decoration-none
        border-bottom
        mb-3
      "
    >
      <span class="tasks-title fs-5 fw-semibold">{{
        selectedTaskList.name
      }}</span>
    </div>

    <t-input
      id="addTaskInput"
      placeholder="Add a task"
      class="mb-3"
      @keyup.enter="onAddTask"
      :model="v$.addTask.name"
    />

    <div class="border-bottom" />

    <div
      class="
        task-group
        list-group list-group-flush
        border-bottom
        scrollarea
        mb-3
      "
    >
      <task-item
        v-for="task in incompleteTasks"
        :key="task.id"
        :task="task"
        @edit="onEditTask"
        @update="onUpdateTask"
        @delete="onDeleteTask"
      />
    </div>

    <t-accordion
      id="completedTasksList"
      header="Completed"
      v-if="hasCompletedTasks"
    >
      <div
        class="
          completed-task-group
          list-group list-group-flush
          border-bottom
          scrollarea
        "
      >
        <task-item
          v-for="task in completedTasks"
          :key="task.id"
          :task="task"
          @update="onUpdateTask"
          @delete="onDeleteTask"
          @edit="onEditTask"
          class="completed-task-item"
        />
      </div>
    </t-accordion>

    <task-modal id="editTaskModal" @save="onUpdateTask" :editTask="editTask" />
  </div>
</template>

<script lang="ts">
import { isUndefinedOrNull } from "../../core/utils/inspect";
import { defineProp, idProp } from "../../core/utils/props";
import IAddTaskRequest from "../../services/tasks/models/IAddTaskRequest";
import TaskDetailViewModel from "../../services/tasks/models/TaskDetailViewModel";
import ITaskDetailViewModel from "../../services/tasks/models/ITaskDetailViewModel";
import tasksService from "../../services/tasks/TasksService";
import TaskItem from "./TaskItem.vue";
import { useVuelidate } from "@vuelidate/core";
import { required } from "@vuelidate/validators";
import _ from "lodash";
import { showErrorMessage } from "../../core/components/messaging/ToastHelper";
import TaskModal from "./TaskModal.vue";

export default {
  setup() {
    return { v$: useVuelidate() };
  },
  components: { TaskItem, TaskModal },
  name: "TasksSidebar",
  props: {
    ...idProp,
    selectedTaskList: defineProp(Object, true),
  },
  data() {
    return {
      editTask: new TaskDetailViewModel(),
      addTask: {
        name: "",
      } as IAddTaskRequest,
    };
  },
  validations() {
    return {
      addTask: {
        name: {
          required,
        },
      },
    };
  },
  computed: {
    taskListId() {
      return this.selectedTaskList?.id;
    },
    tasks() {
      return !isUndefinedOrNull(this.selectedTaskList)
        ? this.selectedTaskList.tasks
        : [];
    },
    incompleteTasks() {
      return _.sortBy(
        this.tasks.filter((task: { completed: boolean }) => !task.completed),
        ["createdDate"],
        ["desc"]
      );
    },
    hasIncompleteTasks() {
      return this.incompletedTasks.length > 0;
    },
    completedTasks() {
      return _.sortBy(
        this.tasks.filter((task: { completed: boolean }) => task.completed),
        ["createdDate"],
        ["desc"]
      );
    },
    hasCompletedTasks() {
      return this.completedTasks.length > 0;
    },
  },
  methods: {
    async onAddTask() {
      this.v$.$touch();
      if (!this.v$.addTask.$invalid) {
        var result = await tasksService.addTaskToList(
          this.taskListId,
          this.addTask
        );

        if (!result.isSuccessStatusCode()) {
          showErrorMessage(this, "Add task failed");
          return;
        } else {
          this.addTask.name = "";
          this.v$.$reset();

          this.$emit("updated");
        }
      }
    },
    onEditTask(task: ITaskDetailViewModel) {
      this.editTask = task;
    },
    async onUpdateTask(task) {
      var result = await tasksService.updateTaskOnList(
        this.selectedTaskList.id,
        task.id,
        task
      );

      if (!result.isSuccessStatusCode()) {
        showErrorMessage(this, "Update task failed");
        return;
      }

      if (task.hideModal) {
        task.hideModal();
      }

      this.$emit("updated", task);
    },
    async onDeleteTask(task: ITaskDetailViewModel) {
      var result = await tasksService.deleteTaskFromList(
        this.selectedTaskList.id,
        task.id
      );

      if (!result.isSuccessStatusCode()) {
        showErrorMessage(this, "Delete task failed");
        return;
      }

      this.$emit("updated", task);
    },
  },
};
</script>

<style lang="scss" scoped>
</style>
