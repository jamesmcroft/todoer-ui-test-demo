export default interface ITaskDetailViewModel {
  id: string;
  name: string;
  note: string;
  dueDate: Date | null;
  important: boolean;
  completed: boolean;
  completedDate: Date | null;
  createdDate: Date
}
