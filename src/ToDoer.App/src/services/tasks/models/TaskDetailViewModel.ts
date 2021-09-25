import ITaskDetailViewModel from "./ITaskDetailViewModel";

export default class TaskDetailViewModel implements ITaskDetailViewModel {
  id: string;
  name: string;
  note: string;
  dueDate: Date;
  important: boolean;
  completed: boolean;
  completedDate: Date;
  createdDate: Date;
}
