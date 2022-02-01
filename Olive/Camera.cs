using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Olive.SceneManagement;
using Vector3 = Olive.Math.Vector3;

namespace Olive;

/// <summary>
///     Represents a camera.
/// </summary>
public sealed class Camera : Component
{
    internal const string MainCameraTag = "MainCamera";

    private Color _clearColor = Color.CornflowerBlue;
    private float _farPlane = 1000f;
    private float _fieldOfView = 90f;
    private float _nearPlane = 0.1f;
    private bool _orthographic = false;
    private float _orthographicSize = 1f;

    /// <summary>
    ///     Gets the current camera.
    /// </summary>
    /// <value>The current camera.</value>
    public static Camera? Current
    {
        get
        {
            Camera? firstCamera = null;

            for (var index = 0; index < SceneManager.LoadedScenes.Count; index++)
            {
                Scene loadedScene = SceneManager.LoadedScenes[index];
                foreach (GameObject gameObject in loadedScene.EnumerateSceneHierarchy())
                {
                    if (!gameObject.TryGetComponent(out Camera? camera))
                    {
                        continue;
                    }

                    if (gameObject.HasTag(MainCameraTag))
                    {
                        return camera;
                    }

                    firstCamera ??= camera;
                }
            }

            return firstCamera;
        }
    }

    /// <summary>
    ///     Gets the main camera.
    /// </summary>
    /// <value>The main camera.</value>
    public static Camera? Main
    {
        get
        {
            for (var index = 0; index < SceneManager.LoadedScenes.Count; index++)
            {
                Scene loadedScene = SceneManager.LoadedScenes[index];
                foreach (GameObject gameObject in loadedScene.EnumerateSceneHierarchy())
                {
                    if (gameObject.HasTag(MainCameraTag) && gameObject.TryGetComponent(out Camera? camera))
                    {
                        return camera;
                    }
                }
            }

            return null;
        }
    }

    /// <summary>
    ///     Gets or sets the camera's clear color.
    /// </summary>
    public Color ClearColor
    {
        get
        {
            OliveEngine.AssertNonDisposed(this);
            return _clearColor;
        }
        set
        {
            OliveEngine.AssertNonDisposed(this);
            _clearColor = value;
        }
    }

    /// <summary>
    ///     Gets or sets the far plane clipping distance.
    /// </summary>
    /// <value>The far plane clipping distance.</value>
    public float FarPlane
    {
        get
        {
            OliveEngine.AssertNonDisposed(this);
            return _farPlane;
        }
        set
        {
            OliveEngine.AssertNonDisposed(this);
            _farPlane = value;
        }
    }

    /// <summary>
    ///     Gets or sets the camera's field of view.
    /// </summary>
    /// <value>The camera's field of view.</value>
    public float FieldOfView
    {
        get
        {
            OliveEngine.AssertNonDisposed(this);
            return _fieldOfView;
        }
        set
        {
            OliveEngine.AssertNonDisposed(this);
            _fieldOfView = value;
        }
    }

    /// <summary>
    ///     Gets or sets the near plane clipping distance.
    /// </summary>
    /// <value>The near plane clipping distance.</value>
    public float NearPlane
    {
        get
        {
            OliveEngine.AssertNonDisposed(this);
            return _nearPlane;
        }
        set
        {
            OliveEngine.AssertNonDisposed(this);
            _nearPlane = value;
        }
    }

    /// <summary>
    ///     Gets or sets a value indicating whether this camera is orthographic.
    /// </summary>
    /// <value><see langword="true" /> if this camera is orthographic, otherwise <see langword="false" />.</value>
    public bool Orthographic
    {
        get
        {
            OliveEngine.AssertNonDisposed(this);
            return _orthographic;
        }
        set
        {
            OliveEngine.AssertNonDisposed(this);
            _orthographic = value;
        }
    }

    /// <summary>
    ///     Gets or sets the camera's orthographic size.
    /// </summary>
    /// <value>The camera's orthographic size.</value>
    /// <remarks>This value is ignored when <see cref="Orthographic" /> is <see langword="false" />.</remarks>
    public float OrthographicSize
    {
        get
        {
            OliveEngine.AssertNonDisposed(this);
            return _orthographicSize;
        }
        set
        {
            OliveEngine.AssertNonDisposed(this);
            _orthographicSize = value;
        }
    }

    internal Matrix GetProjectionMatrix(GraphicsDevice graphicsDevice)
    {
        if (_orthographic)
        {
            return Matrix.CreateOrthographic(_orthographicSize, _orthographicSize, _nearPlane, _farPlane);
        }

        float fovRads = MathHelper.ToRadians(_fieldOfView);
        return Matrix.CreatePerspectiveFieldOfView(fovRads, graphicsDevice.Viewport.AspectRatio, _nearPlane, _farPlane);
    }

    internal Matrix GetViewMatrix()
    {
        Transform transform = Transform;
        Vector3 position = transform.Position;
        return Matrix.CreateLookAt(position, position + transform.Forward, transform.Up);
    }

    internal Matrix GetWorldMatrix()
    {
        return Matrix.CreateTranslation(-Transform.Position);
    }
}
