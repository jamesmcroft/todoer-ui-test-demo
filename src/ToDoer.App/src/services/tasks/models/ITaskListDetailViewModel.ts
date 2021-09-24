import ITaskDetailViewModel from "./ITaskDetailViewModel";
import ITaskListSummaryViewModel from "./ITaskListSummaryViewModel";

export default interface ITaskListDetailViewModel extends ITaskListSummaryViewModel {
    tasks: Array<ITaskDetailViewModel>;
}
