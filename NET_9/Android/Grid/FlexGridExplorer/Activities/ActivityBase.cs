using Android.Content;
using Android.Content.PM;
using Android.Runtime;

namespace FlexGridExplorer
{
    public abstract class ActivityBase : Activity
    {
        static Dictionary<int, TaskCompletionSource<Intent>> _runningActivities = new Dictionary<int, TaskCompletionSource<Intent>>();
        static Dictionary<int, TaskCompletionSource<Permission[]>> _requestedPermissions = new Dictionary<int, TaskCompletionSource<Permission[]>>();
        Random _rand = new Random();


        protected override async void OnResume()
        {
            base.OnResume();
            if (_resumeTask != null)
            {
                _resumeTask.TrySetResult(true);
                _resumeTask = null;
            }
        }

        TaskCompletionSource<bool> _resumeTask;
        private async Task WaitUntilResume()
        {
            if (_resumeTask == null)
                _resumeTask = new TaskCompletionSource<bool>();
            await _resumeTask.Task;
        }

        protected override async void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);
            await WaitUntilResume();
            TaskCompletionSource<Intent> tcs;
            if (_runningActivities.TryGetValue(requestCode, out tcs))
            {
                _runningActivities.Remove(requestCode);
                if (resultCode == Result.Ok)
                {
                    tcs.TrySetResult(data);
                }
                else
                {
                    tcs.TrySetCanceled();
                }
            }
            //else if (_callbackManager != null)
            //{
            //    _callbackManager.OnActivityResult(requestCode, (int)resultCode, data);
            //}
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Permission[] grantResults)
        {
            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
            TaskCompletionSource<Permission[]> tcs;
            if (_requestedPermissions.TryGetValue(requestCode, out tcs))
            {
                _requestedPermissions.Remove(requestCode);
                if (grantResults.Length > 0)
                {
                    tcs.TrySetResult(grantResults);
                }
                else
                {
                    tcs.TrySetCanceled();
                }
            }
        }
        public async Task<Intent> StartActivityForResultAsync(Intent intent)
        {
            var tcs = new TaskCompletionSource<Intent>();
            try
            {
                ushort requestCode = (ushort)_rand.Next();
                _runningActivities.Add(requestCode, tcs);
                //intent.SetFlags(ActivityFlags.ForwardResult);
                StartActivityForResult(intent, requestCode);
            }
            catch (Exception exc)
            {
                tcs.TrySetException(exc);
            }
            return await tcs.Task;
        }

        public async Task<Permission[]> RequestPermissionsAsync(string[] permissions)
        {
            var tcs = new TaskCompletionSource<Permission[]>();
            try
            {
                ushort requestCode = (ushort)_rand.Next();
                _requestedPermissions.Add(requestCode, tcs);
                RequestPermissions(permissions, requestCode);
            }
            catch (Exception exc)
            {
                tcs.TrySetException(exc);
            }
            return await tcs.Task;
        }
    }
}
