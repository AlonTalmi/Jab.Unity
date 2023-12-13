An extensions package to better integrate [Jab](https://github.com/pakrym/jab) with Unity.

Currently includes:
 - IInjector interface that when registered to a ServiceProvider adds inject functionallity through extension methods
 - When IInjector is registered allows InjectScene, InjectGameObject, Instantiate, etc'
 - Added 2 types of injectors: ReflectionInjector and a super simple ManualInjector (hopefully will add source generated injector in the future)

Todo:
  - Unit Tests and runtime tests (both for Jab in general inside Unity and for the extensions)
  - Add a simple way to create a serializeable project/scene/gameobject contexts
  - Hoping to add somekind of ServiceProvider editor to edit attributes straight from the editor
    


To use add https://github.com/AlonTalmi/Jab.Unity.git through UPM

(Window ↝ Package Manager ↝ '+' ↝ Add package from git URL...)
