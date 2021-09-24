export default interface IUpdateTaskRequest {
  name: string;
  note: string;
  dueDate: Date | null;
  important: boolean;
  completed: boolean;
}
