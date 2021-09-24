import { JsonDeleteRequest, JsonGetRequest, JsonPostRequest, JsonPutRequest, NetworkResponse } from 'made-networking';
import axiosClient from '../../core/networking/AxiosClient';
import IAddTaskListRequest from './models/IAddTaskListRequest';
import IAddTaskRequest from './models/IAddTaskRequest';
import ITaskListDetailViewModel from './models/ITaskListDetailViewModel';
import ITaskListSummaryViewModel from './models/ITaskListSummaryViewModel';
import IUpdateTaskListRequest from './models/IUpdateTaskListRequest';
import IUpdateTaskRequest from './models/IUpdateTaskRequest';

class TasksService {
  tasksApiVersion = '1.0';

  endpoints = {
    tasks: {
      getMyTaskLists: 'api/tasks/my',
      getTaskList: (taskListId: string) => `api/tasks/list/${taskListId}`,
      addTaskList: 'api/tasks/list',
      updateTaskList: (taskListId: string) => `api/tasks/list/${taskListId}`,
      deleteTaskList: (taskListId: string) => `api/tasks/list/${taskListId}`,
      addTaskToTaskList: (taskListId: string) => `api/tasks/list/${taskListId}`,
      updateTaskOnTaskList: (taskListId: string, taskId: string) => `api/tasks/list/${taskListId}/${taskId}`,
      deleteTaskFromTaskList: (taskListId: string, taskId: string) => `api/tasks/list/${taskListId}/${taskId}`,
    }
  }

  async getMyTaskLists(): Promise<NetworkResponse<ITaskListSummaryViewModel[]>> {
    return await new JsonGetRequest(axiosClient, this.endpoints.tasks.getMyTaskLists, { ApiVersion: this.tasksApiVersion }, {}).execute<ITaskListSummaryViewModel[]>();
  }

  async getTaskList(taskListId: string): Promise<NetworkResponse<ITaskListDetailViewModel>> {
    return await new JsonGetRequest(axiosClient, this.endpoints.tasks.getTaskList(taskListId), { ApiVersion: this.tasksApiVersion }, {}).execute<ITaskListDetailViewModel>();
  }

  async addTaskList(addTaskList: IAddTaskListRequest) {
    return await new JsonPostRequest(axiosClient, this.endpoints.tasks.addTaskList, addTaskList, { ApiVersion: this.tasksApiVersion }).execute();
  }

  async updateTaskList(taskListId: string, updateTaskList: IUpdateTaskListRequest) {
    return await new JsonPutRequest(axiosClient, this.endpoints.tasks.updateTaskList(taskListId), updateTaskList, { ApiVersion: this.tasksApiVersion }).execute();
  }

  async deleteTaskList(taskListId: string) {
    return await new JsonDeleteRequest(axiosClient, this.endpoints.tasks.deleteTaskList(taskListId), { ApiVersion: this.tasksApiVersion }, {}).execute();
  }

  async addTaskToList(taskListId: string, addTask: IAddTaskRequest) {
    return await new JsonPostRequest(axiosClient, this.endpoints.tasks.addTaskToTaskList(taskListId), addTask, { ApiVersion: this.tasksApiVersion }).execute();
  }

  async updateTaskOnList(taskListId: string, taskId: string, updateTask: IUpdateTaskRequest) {
    return await new JsonPutRequest(axiosClient, this.endpoints.tasks.updateTaskOnTaskList(taskListId, taskId), updateTask, { ApiVersion: this.tasksApiVersion }).execute();
  }

  async deleteTaskFromList(taskListId: string, taskId: string) {
    return await new JsonDeleteRequest(axiosClient, this.endpoints.tasks.deleteTaskFromTaskList(taskListId, taskId), { ApiVersion: this.tasksApiVersion }, {}).execute();
  }
}

const tasksService = new TasksService();
export default tasksService;
