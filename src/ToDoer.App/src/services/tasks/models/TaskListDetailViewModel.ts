import ITaskDetailViewModel from "./ITaskDetailViewModel";
import ITaskListDetailViewModel from "./ITaskListDetailViewModel";

export default class TaskListDetailViewModel implements ITaskListDetailViewModel {
  tasks: ITaskDetailViewModel[] = [];
  id: string;
  name: string;
}
