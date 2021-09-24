<template>
  <div class="row">
    <div class="col-4">
      <task-list-sidebar
        id="taskListSidebar"
        :taskLists="taskLists"
        :selectedTaskList="selectedTaskList"
        @selected="onTaskListSelected"
      />
    </div>
    <div class="col-8">
      <tasks-sidebar
        id="tasksSidebar"
        :selectedTaskList="selectedTaskList"
        v-if="selectedTaskList"
      />
    </div>
  </div>
</template>

<script lang="ts">
import TaskListSidebar from "../components/tasks/TaskListSidebar.vue";
import TasksSidebar from "../components/tasks/TasksSidebar.vue";
import { isUndefinedOrNull } from "../core/utils/inspect";
import authenticationService from "../services/authentication/AuthenticationService";
import ITaskListDetailViewModel from "../services/tasks/models/ITaskListDetailViewModel";
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
      if (!result.isSuccessStatusCode) {
        console.log(result);
      } else {
        this.taskLists = result.content;
      }
      this.isLoading = false;
    },
    async onTaskListSelected(taskList: ITaskListSummaryViewModel) {
      if (!isUndefinedOrNull(taskList)) {
        var result = await tasksService.getTaskList(taskList.id);
        if (!result.isSuccessStatusCode) {
          console.log(result);
        } else {
          this.selectedTaskList = result.content;
        }
      } else {
        this.selectedTaskList = null;
      }
    },
  },
};
</script>

<style lang="scss" scoped>
</style>
