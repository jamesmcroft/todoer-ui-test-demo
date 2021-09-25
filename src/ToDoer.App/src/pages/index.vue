<template>
  <div class="row">
    <div class="col-4">
      <task-list-sidebar
        id="taskListSidebar"
        :taskLists="taskLists"
        :selectedTaskList="selectedTaskList"
        @selected="onTaskListSelected"
        @updated="onTaskListUpdated"
      />
    </div>
    <div class="col-8">
      <tasks-sidebar
        id="tasksSidebar"
        :selectedTaskList="selectedTaskList"
        v-if="selectedTaskList?.id"
        @updated="onTaskUpdated"
      />
    </div>
  </div>
</template>

<script lang="ts">
import TaskListSidebar from "../components/tasks/TaskListSidebar.vue";
import TasksSidebar from "../components/tasks/TasksSidebar.vue";
import { showErrorMessage } from "../core/components/messaging/ToastHelper";
import { isUndefinedOrNull } from "../core/utils/inspect";
import authenticationService from "../services/authentication/AuthenticationService";
import ITaskDetailViewModel from "../services/tasks/models/ITaskDetailViewModel";
import ITaskListDetailViewModel from "../services/tasks/models/ITaskListDetailViewModel";
import TaskListDetailViewModel from "../services/tasks/models/TaskListDetailViewModel";
import ITaskListSummaryViewModel from "../services/tasks/models/ITaskListSummaryViewModel";
import tasksService from "../services/tasks/TasksService";

export default {
  name: "Home",
  components: { TaskListSidebar, TasksSidebar },
  data() {
    return {
      taskLists: [] as ITaskListSummaryViewModel[],
      selectedTaskList: null as ITaskListDetailViewModel,
      isLoading: false,
    };
  },
  async mounted() {
    if (authenticationService.hasAuthCookie()) {
      await this.load();
    }
  },
  methods: {
    async load() {
      this.isLoading = true;
      var result = await tasksService.getMyTaskLists();
      if (!result.isSuccessStatusCode()) {
        showErrorMessage(this, "Couldn't retrieve task lists");
      } else {
        this.taskLists = result.content;
      }
      this.isLoading = false;
    },
    async onTaskListSelected(taskList: ITaskListSummaryViewModel) {
      if (!isUndefinedOrNull(taskList)) {
        var result = await tasksService.getTaskList(taskList.id);
        if (!result.isSuccessStatusCode()) {
          showErrorMessage(this, "Couldn't retrieve task list details");
        } else {
          this.selectedTaskList = result.content;
        }
      } else {
        this.selectedTaskList = new TaskListDetailViewModel();
      }
    },
    async onTaskUpdated(task: ITaskDetailViewModel) {
      await this.onTaskListSelected(this.selectedTaskList);
    },
    async onTaskListUpdated(taskList: ITaskListDetailViewModel) {
      await this.load();
    },
  },
};
</script>

<style lang="scss" scoped>
</style>
