using Fluxor;

namespace WorkoutTrackerApp.State.UploadWorkouts
{
    public static class UWReducer
    {
        [ReducerMethod]
        public static UWState OnFileChange(UWState state, UploadFileAction action)
        {
            return state;
        }

        [ReducerMethod]
        public static UWState OnFileLoaded(UWState state, FilesLoadedAction action)
        {
            return state with
            {
                FileName = action.FileName,
                Workouts = action.Workouts
            };
        }

        [ReducerMethod]
        public static UWState OnInvalidFile(UWState state, UploadFileErrorAction action)
        {
            return state with
            {
                FileName = action.ErrorMessage
            };
        }

    }
}
