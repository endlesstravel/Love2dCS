
#region Physics
using System;

namespace Love
{

    public class Body : LoveObject
    {
        /// <summary>
        /// disable construct
        /// </summary>
        protected Body() { }
        public Vector3 GetTransform()
        {
            Vector3 out_pos;
            Love2dDll.wrap_love_dll_type_Body_getTransform(p, out out_pos);
            return out_pos;
        }

        public Vector2 GetLinearVelocity()
        {
            Vector2 out_result;
            Love2dDll.wrap_love_dll_type_Body_getLinearVelocity(p, out out_result);
            return out_result;
        }

        public Vector2 GetWorldCenter()
        {
            Vector2 out_result;
            Love2dDll.wrap_love_dll_type_Body_getWorldCenter(p, out out_result);
            return out_result;
        }

        public Vector2 GetLocalCenter()
        {
            Vector2 out_result;
            Love2dDll.wrap_love_dll_type_Body_getLocalCenter(p, out out_result);
            return out_result;
        }

        public float GetAngularVelocity()
        {
            float out_result;
            Love2dDll.wrap_love_dll_type_Body_getAngularVelocity(p, out out_result);
            return out_result;
        }

        public float GetMass()
        {
            float out_result;
            Love2dDll.wrap_love_dll_type_Body_getMass(p, out out_result);
            return out_result;
        }

        public float GetInertia()
        {
            float out_result;
            Love2dDll.wrap_love_dll_type_Body_getInertia(p, out out_result);
            return out_result;
        }

        public float GetAngularDamping()
        {
            float out_result;
            Love2dDll.wrap_love_dll_type_Body_getAngularDamping(p, out out_result);
            return out_result;
        }

        public float GetLinearDamping()
        {
            float out_result;
            Love2dDll.wrap_love_dll_type_Body_getLinearDamping(p, out out_result);
            return out_result;
        }

        public float GetGravityScale()
        {
            float out_result;
            Love2dDll.wrap_love_dll_type_Body_getGravityScale(p, out out_result);
            return out_result;
        }

        public BodyType GetBodyType()
        {
            int out_body_type;
            Love2dDll.wrap_love_dll_type_Body_getType(p, out out_body_type);
            return (BodyType)out_body_type;
        }

        public void ApplyLinearImpulse(float jx, float jy)
        {
            Love2dDll.wrap_love_dll_type_Body_applyLinearImpulse_xy(p, jx, jy);
        }

        public void ApplyLinearImpulse(float jx, float jy, float ox, float oy)
        {
            Love2dDll.wrap_love_dll_type_Body_applyLinearImpulse_xy_offset(p, jx, jy, ox, oy);
        }

        public void ApplyAngularImpulse(float i)
        {
            Love2dDll.wrap_love_dll_type_Body_applyAngularImpulse(p, i);
        }

        public void ApplyTorque(float torque)
        {
            Love2dDll.wrap_love_dll_type_Body_applyTorque(p, torque);
        }

        public void ApplyForce(float fx, float fy)
        {
            Love2dDll.wrap_love_dll_type_Body_applyForce_xy(p, fx, fy);
        }

        public void ApplyForce(float fx, float fy, float ox, float oy)
        {
            Love2dDll.wrap_love_dll_type_Body_applyForce_xy_offset(p, fx, fy, ox, oy);
        }

        public void SetX(float x)
        {
            Love2dDll.wrap_love_dll_type_Body_setX(p, x);
        }

        public void SetY(float y)
        {
            Love2dDll.wrap_love_dll_type_Body_setY(p, y);
        }

        public void SetLinearVelocity(float x, float y)
        {
            Love2dDll.wrap_love_dll_type_Body_setLinearVelocity(p, x, y);
        }

        public void SetAngle(float angle)
        {
            Love2dDll.wrap_love_dll_type_Body_setAngle(p, angle);
        }

        public void SetAngularVelocity(float angleVelocity)
        {
            Love2dDll.wrap_love_dll_type_Body_setAngularVelocity(p, angleVelocity);
        }

        public void SetPosition(float x, float y)
        {
            Love2dDll.wrap_love_dll_type_Body_setPosition(p, x, y);
        }

        public void ResetMassData()
        {
            Love2dDll.wrap_love_dll_type_Body_resetMassData(p);
        }

        public void SetMass(float m)
        {
            Love2dDll.wrap_love_dll_type_Body_setMass(p, m);
        }

        public void SetInertia(float inertia)
        {
            Love2dDll.wrap_love_dll_type_Body_setInertia(p, inertia);
        }

        public void SetAngularDamping(float angularDamping)
        {
            Love2dDll.wrap_love_dll_type_Body_setAngularDamping(p, angularDamping);
        }

        public void SetLinearDamping(float linerDamping)
        {
            Love2dDll.wrap_love_dll_type_Body_setLinearDamping(p, linerDamping);
        }

        public void SetGravityScale(float scale)
        {
            Love2dDll.wrap_love_dll_type_Body_setGravityScale(p, scale);
        }

        public void SetType(int body_type)
        {
            Love2dDll.wrap_love_dll_type_Body_setType(p, body_type);
        }

        public Vector2 GetWorldPoint(float x, float y)
        {
            Vector2 out_result;
            Love2dDll.wrap_love_dll_type_Body_getWorldPoint(p, x, y, out out_result);
            return out_result;
        }

        public Vector2 GetWorldVector(float x, float y)
        {
            Vector2 out_result;
            Love2dDll.wrap_love_dll_type_Body_getWorldVector(p, x, y, out out_result);
            return out_result;
        }

        public Vector2 GetLocalPoint(float x, float y)
        {
            Vector2 out_result;
            Love2dDll.wrap_love_dll_type_Body_getLocalPoint(p, x, y, out out_result);
            return out_result;
        }

        public Vector2 GetLocalVector(float x, float y)
        {
            Vector2 out_result;
            Love2dDll.wrap_love_dll_type_Body_getLocalVector(p, x, y, out out_result);
            return out_result;
        }

        public Vector2 GetLinearVelocityFromWorldPoint(float x, float y)
        {
            Vector2 out_result;
            Love2dDll.wrap_love_dll_type_Body_getLinearVelocityFromWorldPoint(p, x, y, out out_result);
            return out_result;
        }

        public Vector2 GetLinearVelocityFromLocalPoint(float x, float y)
        {
            Vector2 out_result;
            Love2dDll.wrap_love_dll_type_Body_getLinearVelocityFromLocalPoint(p, x, y, out out_result);
            return out_result;
        }

        public bool IsBullet()
        {
            bool out_result;
            Love2dDll.wrap_love_dll_type_Body_isBullet(p, out out_result);
            return out_result;
        }

        public void SetBullet(bool b)
        {
            Love2dDll.wrap_love_dll_type_Body_setBullet(p, b);
        }

        public bool IsActive()
        {
            bool out_result;
            Love2dDll.wrap_love_dll_type_Body_isActive(p, out out_result);
            return out_result;
        }

        public bool IsAwake()
        {
            bool out_result;
            Love2dDll.wrap_love_dll_type_Body_isAwake(p, out out_result);
            return out_result;
        }

        public void SetSleepingAllowed(bool b)
        {
            Love2dDll.wrap_love_dll_type_Body_setSleepingAllowed(p, b);
        }

        public bool IsSleepingAllowed()
        {
            bool out_result;
            Love2dDll.wrap_love_dll_type_Body_isSleepingAllowed(p, out out_result);
            return out_result;
        }

        public void SetActive(bool b)
        {
            Love2dDll.wrap_love_dll_type_Body_setActive(p, b);
        }

        public void SetAwake(bool b)
        {
            Love2dDll.wrap_love_dll_type_Body_setAwake(p, b);
        }

        public void SetFixedRotation(bool b)
        {
            Love2dDll.wrap_love_dll_type_Body_setFixedRotation(p, b);
        }

        public bool IsFixedRotation()
        {
            bool out_result;
            Love2dDll.wrap_love_dll_type_Body_isFixedRotation(p, out out_result);
            return out_result;
        }

        public World GetWorld()
        {
            System.IntPtr out_world;
            Love2dDll.wrap_love_dll_type_Body_getWorld(p, out out_world);
            return NewObject<World>(out_world);
        }

        public void Destroy()
        {
            Love2dDll.wrap_love_dll_type_Body_destroy(p);
        }

        public bool IsDestroyed()
        {
            bool out_result;
            Love2dDll.wrap_love_dll_type_Body_isDestroyed(p, out out_result);
            return out_result;
        }

        public Fixture[] GetFixtureList()
        {
            Love2dDll.wrap_love_dll_type_Body_getFixtureList(p, out var pfixtures, out int fixture_length);
            return DllTool.ReadIntPtrsWithConvertAndRelease<Fixture>(pfixtures, fixture_length);
        }

        public Joint[] GetJointList()
        {
            Love2dDll.wrap_love_dll_type_Body_getJointList(p, out var pfixtures, out int fixture_length);
            return DllTool.ReadIntPtrsWithConvertAndRelease<Joint>(pfixtures, fixture_length);
        }

        public Contact[] GetContactList()
        {
            Love2dDll.wrap_love_dll_type_Body_getContactList(p, out var pfixtures, out int fixture_length);
            return DllTool.ReadIntPtrsWithConvertAndRelease<Contact>(pfixtures, fixture_length);
        }
    }
    public class Contact : LoveObject
    {
        /// <summary>
        /// disable construct
        /// </summary>
        protected Contact() { }

        public float GetFriction()
        {
            float out_friction;
            Love2dDll.wrap_love_dll_type_Contact_getFriction(p, out out_friction);
            return out_friction;
        }

        public float GetRestitution()
        {
            float out_restitution;
            Love2dDll.wrap_love_dll_type_Contact_getRestitution(p, out out_restitution);
            return out_restitution;
        }

        public bool IsEnabled()
        {
            bool out_result;
            Love2dDll.wrap_love_dll_type_Contact_isEnabled(p, out out_result);
            return out_result;
        }

        public bool IsTouching()
        {
            bool out_result;
            Love2dDll.wrap_love_dll_type_Contact_isTouching(p, out out_result);
            return out_result;
        }

        public void SetFriction(float friction)
        {
            Love2dDll.wrap_love_dll_type_Contact_setFriction(p, friction);
        }

        public void SetRestitution(float restitution)
        {
            Love2dDll.wrap_love_dll_type_Contact_setRestitution(p, restitution);
        }

        public void SetEnabled(bool enabled)
        {
            Love2dDll.wrap_love_dll_type_Contact_setEnabled(p, enabled);
        }

        public void ResetFriction()
        {
            Love2dDll.wrap_love_dll_type_Contact_resetFriction(p);
        }

        public void ResetRestitution()
        {
            Love2dDll.wrap_love_dll_type_Contact_resetRestitution(p);
        }

        public void SetTangentSpeed(float speed)
        {
            Love2dDll.wrap_love_dll_type_Contact_setTangentSpeed(p, speed);
        }

        public float GetTangentSpeed()
        {
            float out_speed;
            Love2dDll.wrap_love_dll_type_Contact_getTangentSpeed(p, out out_speed);
            return out_speed;
        }

        public void GetChildren(out int indexA, out int indexB)
        {
            Love2dDll.wrap_love_dll_type_Contact_getChildren(p, out indexA, out indexB);
        }

        public void GetFixtures(out Fixture fixtureA, out Fixture fixtureB)
        {
            Love2dDll.wrap_love_dll_type_Contact_getFixtures(p, out var pa, out var pb);
            fixtureA = NewObject<Fixture>(pa);
            fixtureB = NewObject<Fixture>(pb);
        }

        public bool IsDestroyed()
        {
            bool out_result;
            Love2dDll.wrap_love_dll_type_Contact_isDestroyed(p, out out_result);
            return out_result;
        }

        public Vector2[] GetPositions()
        {
            Love2dDll.wrap_love_dll_type_Contact_getPositions(p, out var pointList, out var pointListLength);
            return DllTool.ReadVector2sAndRelease(pointList, pointListLength);
        }

        public Vector2 GetNormal()
        {
            Love2dDll.wrap_love_dll_type_Contact_getNormal(p, out float nx, out float ny);
            return new Vector2(nx, ny);
        }
    }
    public class Fixture : LoveObject
    {
        /// <summary>
        /// disable construct
        /// </summary>
        protected Fixture() { }

        public ShapeType GetShapeType()
        {
            int out_fixture_type;
            Love2dDll.wrap_love_dll_type_Fixture_getType(p, out out_fixture_type);
            return (ShapeType)out_fixture_type;
        }

        public void SetFriction(float friction)
        {
            Love2dDll.wrap_love_dll_type_Fixture_setFriction(p, friction);
        }

        public void SetRestitution(float restitution)
        {
            Love2dDll.wrap_love_dll_type_Fixture_setRestitution(p, restitution);
        }

        public void SetDensity(float density)
        {
            Love2dDll.wrap_love_dll_type_Fixture_setDensity(p, density);
        }

        public void SetSensor(bool sensor)
        {
            Love2dDll.wrap_love_dll_type_Fixture_setSensor(p, sensor);
        }

        public float GetFriction()
        {
            float out_result;
            Love2dDll.wrap_love_dll_type_Fixture_getFriction(p, out out_result);
            return out_result;
        }

        public float GetRestitution()
        {
            float out_result;
            Love2dDll.wrap_love_dll_type_Fixture_getRestitution(p, out out_result);
            return out_result;
        }

        public float GetDensity()
        {
            float out_result;
            Love2dDll.wrap_love_dll_type_Fixture_getDensity(p, out out_result);
            return out_result;
        }

        public bool IsSensor()
        {
            bool out_result;
            Love2dDll.wrap_love_dll_type_Fixture_isSensor(p, out out_result);
            return out_result;
        }

        public Body GetBody()
        {
            System.IntPtr out_body;
            Love2dDll.wrap_love_dll_type_Fixture_getBody(p, out out_body);
            return NewObject<Body>(out_body);
        }

        public Shape GetShape()
        {
            System.IntPtr out_shape;
            Love2dDll.wrap_love_dll_type_Fixture_getShape(p, out out_shape);
            return NewObject<Shape>(out_shape);
        }

        public bool TestPoint(float x, float y)
        {
            bool out_result;
            Love2dDll.wrap_love_dll_type_Fixture_testPoint(p, x, y, out out_result);
            return out_result;
        }

        public void SetFilterData(float categories, float mask, float group)
        {
            Love2dDll.wrap_love_dll_type_Fixture_setFilterData(p, categories, mask, group);
        }

        public void GetFilterData(out float categories, out float mask, out float group)
        {
            Love2dDll.wrap_love_dll_type_Fixture_getFilterData(p, out categories, out mask, out group);
        }

        public int GetGroupIndex()
        {
            int out_index;
            Love2dDll.wrap_love_dll_type_Fixture_getGroupIndex(p, out out_index);
            return out_index;
        }

        public void SetGroupIndex(int index)
        {
            Love2dDll.wrap_love_dll_type_Fixture_setGroupIndex(p, index);
        }

        public void Destroy()
        {
            Love2dDll.wrap_love_dll_type_Fixture_destroy(p);
        }

        public bool IsDestroyed()
        {
            bool out_result;
            Love2dDll.wrap_love_dll_type_Fixture_isDestroyed(p, out out_result);
            return out_result;
        }

        public bool RayCast(float x1, float y1, float x2, float y2, float maxFraction, int childIndex, out Vector2 pos, out float fraction)
        {
            Love2dDll.wrap_love_dll_type_Fixture_rayCast(p, x1, y1, x2, y2, maxFraction, childIndex, out bool out_result, out pos, out fraction);
            return out_result;
        }

        public void SetCategory(ushort categories)
        {
            Love2dDll.wrap_love_dll_type_Fixture_setCategory(p, categories);
        }

        public ushort GetCategory()
        {
            ushort out_categories;
            Love2dDll.wrap_love_dll_type_Fixture_getCategory(p, out out_categories);
            return out_categories;
        }

        public void SetMask(ushort masks)
        {
            Love2dDll.wrap_love_dll_type_Fixture_setMask(p, masks);
        }

        public ushort GetMask()
        {
            UInt16 out_mask;
            Love2dDll.wrap_love_dll_type_Fixture_getMask(p, out out_mask);
            return out_mask;
        }

        public RectangleF GetBoundingBox(int childIndex)
        {
            Love2dDll.wrap_love_dll_type_Fixture_getBoundingBox(p, childIndex,
                out float lx, out float ly, out float ux, out float uy);
            return new RectangleF(lx, ly, ux - lx, uy - ly);
        }

        public void GetMassData(out Vector2 center, out float mass, out float rotationalInertia)
        {
            Love2dDll.wrap_love_dll_type_Fixture_getMassData(p, out center, out mass, out rotationalInertia);
        }

    }
    public class Shape : LoveObject
    {
        /// <summary>
        /// disable construct
        /// </summary>
        protected Shape() { }
        public int GetType()
        {
            int out_shapeType;
            Love2dDll.wrap_love_dll_type_Shape_getType(p, out out_shapeType);
            return out_shapeType;
        }

        public float GetRadius()
        {
            float out_radius;
            Love2dDll.wrap_love_dll_type_Shape_getRadius(p, out out_radius);
            return out_radius;
        }

        public float GetChildCount()
        {
            float out_childCount;
            Love2dDll.wrap_love_dll_type_Shape_getChildCount(p, out out_childCount);
            return out_childCount;
        }

        public bool TestPoint(float tx, float ty, float tr, float px, float py)
        {
            bool out_result;
            Love2dDll.wrap_love_dll_type_Shape_testPoint(p, tx, ty, tr, px, py, out out_result);
            return out_result;
        }

        public delegate float RayCastCallBack(float nx, float ny, float fraction);

        public void RayCast(Vector2 p1, Vector2 p2, float maxFraction, Vector2 trans, float tr, int childIndex, RayCastCallBack callback)
        {
            Love2dDll.wrap_love_dll_type_Shape_rayCast(p, p1, p2, maxFraction, trans, tr, childIndex, (nx, ny, fraction) => callback(nx, ny, fraction));
        }

        public delegate float ComputeAABBCallback(float lx, float ly, float ux, float uy);
        public void ComputeAABB(float x, float y, float r, int childIndex, ComputeAABBCallback callback)
        {
            Love2dDll.wrap_love_dll_type_Shape_computeAABB(p, x, y, r, childIndex, (lx, ly, ux, uy) => callback(lx, ly, ux, uy));
        }

        public delegate float ComputeMassCallback(float x, float y, float mass, float inertia);
        public void ComputeMass(float density, ComputeMassCallback callback)
        {
            Love2dDll.wrap_love_dll_type_Shape_computeMass(p, density, (x, y, m, inertia) => callback(x, y, m, inertia));
        }

    }
    public class Joint : LoveObject
    {
        /// <summary>
        /// disable construct
        /// </summary>
        protected Joint() { }

        public JointType GetJointType()
        {
            int out_type;
            Love2dDll.wrap_love_dll_type_Joint_getType(p, out out_type);
            return (JointType)out_type;
        }

        public void GetBodies(out Body bodyA, out Body bodyB)
        {
            Love2dDll.wrap_love_dll_type_Joint_getBodies(p, out var pa, out var pb);
            bodyA = NewObject<Body>(pa);
            bodyB = NewObject<Body>(pb);
        }

        public float GetReactionTorque(float inv_dt)
        {
            float out_torque;
            Love2dDll.wrap_love_dll_type_Joint_getReactionTorque(p, inv_dt, out out_torque);
            return out_torque;
        }

        public bool GetCollideConnected()
        {
            bool out_c;
            Love2dDll.wrap_love_dll_type_Joint_getCollideConnected(p, out out_c);
            return out_c;
        }

        public void Destroy()
        {
            Love2dDll.wrap_love_dll_type_Joint_destroy(p);
        }

        public bool IsDestroyed()
        {
            bool out_destroyed;
            Love2dDll.wrap_love_dll_type_Joint_isDestroyed(p, out out_destroyed);
            return out_destroyed;
        }

        public void GetAnchors(out float x1, out float y1, out float x2, out float y2)
        {
            Love2dDll.wrap_love_dll_type_Joint_getAnchors(p, out x1, out y1, out x2, out y2);
        }

        public Vector2 GetReactionForce(float dt)
        {
            Love2dDll.wrap_love_dll_type_Joint_getReactionForce(p, dt, out float x, out float y);
            return new Vector2(x, y);
        }

        internal static Joint JointRetainToRegularJoint(Joint joint)
        {
            switch (joint.GetJointType())
            {
                case JointType.Distance: RetainLoveObject(joint.p); return NewObject<DistanceJoint>(joint.p);
                case JointType.Revolute: RetainLoveObject(joint.p); return NewObject<RevoluteJoint>(joint.p);
                case JointType.Prismatic: RetainLoveObject(joint.p); return NewObject<PrismaticJoint>(joint.p);
                case JointType.Mouse: RetainLoveObject(joint.p); return NewObject<MouseJoint>(joint.p);
                case JointType.Pulley: RetainLoveObject(joint.p); return NewObject<PulleyJoint>(joint.p);
                case JointType.Gear: RetainLoveObject(joint.p); return NewObject<GearJoint>(joint.p);
                case JointType.Friction: RetainLoveObject(joint.p); return NewObject<FrictionJoint>(joint.p);
                case JointType.Weld: RetainLoveObject(joint.p); return NewObject<WeldJoint>(joint.p);
                case JointType.Wheel: RetainLoveObject(joint.p); return NewObject<WheelJoint>(joint.p);
                case JointType.Rope: RetainLoveObject(joint.p); return NewObject<RopeJoint>(joint.p);
                case JointType.Motor: RetainLoveObject(joint.p); return NewObject<MotorJoint>(joint.p);
            }

            return null;
        }
        internal static Joint[] JointRetainToRegularJoint(Joint[] jointList)
        {
            Joint[] finalList = new Joint[jointList.Length];
            for (int i = 0; i < jointList.Length; i++)
            {
                finalList[i] = JointRetainToRegularJoint(jointList[i]);
            }
            return finalList;
        }

    }
    public class World : LoveObject
    {
        /// <summary>
        /// disable construct
        /// </summary>
        protected World() { }
        public void SetGravity(float gx, float gy)
        {
            Love2dDll.wrap_love_dll_type_World_setGravity(p, gx, gy);
        }

        public void TranslateOrigin(float x, float y)
        {
            Love2dDll.wrap_love_dll_type_World_translateOrigin(p, x, y);
        }

        public void SetSleepingAllowed(bool allowed)
        {
            Love2dDll.wrap_love_dll_type_World_setSleepingAllowed(p, allowed);
        }

        public bool IsSleepingAllowed()
        {
            bool out_allowed;
            Love2dDll.wrap_love_dll_type_World_isSleepingAllowed(p, out out_allowed);
            return out_allowed;
        }

        public bool IsLocked()
        {
            bool out_locked;
            Love2dDll.wrap_love_dll_type_World_isLocked(p, out out_locked);
            return out_locked;
        }

        public int GetBodyCount()
        {
            int out_count;
            Love2dDll.wrap_love_dll_type_World_getBodyCount(p, out out_count);
            return out_count;
        }

        public int GetJointCount()
        {
            int out_count;
            Love2dDll.wrap_love_dll_type_World_getJointCount(p, out out_count);
            return out_count;
        }

        public int GetContactCount()
        {
            int out_count;
            Love2dDll.wrap_love_dll_type_World_getContactCount(p, out out_count);
            return out_count;
        }

        public void Destroy()
        {
            Love2dDll.wrap_love_dll_type_World_destroy(p);
        }

        public bool IsDestroyed()
        {
            bool out_validate;
            Love2dDll.wrap_love_dll_type_World_isDestroyed(p, out out_validate);
            return out_validate;
        }

        public delegate void WorldContactCallback(Fixture fixtureA, Fixture fixtureB, Contact contact);
        public delegate void WorldContactPostSolveCallback(Fixture fixtureA, Fixture fixtureB, Contact contact, Vector2[] impluseArray);
        public delegate bool WorldContactFilterCallback(Fixture fixtureA, Fixture fixtureB);

        WorldContactCallback beginContact;
        WorldContactCallback endContact;
        WorldContactCallback preSolve;
        WorldContactPostSolveCallback postSolve;

        public void SetCallbacks(
            WorldContactCallback beginContact,
            WorldContactCallback endContact,
            WorldContactCallback preSolve,
            WorldContactPostSolveCallback postSolve)
        {
            this.beginContact = beginContact;
            this.endContact = endContact;
            this.preSolve = preSolve;
            this.postSolve = postSolve;
        }

        public void GetCallbacks(
            out WorldContactCallback beginContact,
            out WorldContactCallback endContact,
            out WorldContactCallback preSolve,
            out WorldContactPostSolveCallback postSolve)
        {
            beginContact = this.beginContact;
            endContact = this.endContact;
            preSolve = this.preSolve;
            postSolve = this.postSolve;
        }

        WorldContactFilterCallback filterCallback;
        public WorldContactFilterCallback GetContactFilter()
        {
            return filterCallback;
        }
        public void SetContactFilter(WorldContactFilterCallback filterCallback)
        {
            this.filterCallback = filterCallback;
        }

        public void Update(float dt, int velocityiterations = 8, int positioniterations = 3)
        {
            WrapWorldContactCallbackDelegate _beginContact = (a, b, c, _, __) => beginContact(NewObject<Fixture>(a), NewObject<Fixture>(b), NewObject<Contact>(c));
            WrapWorldContactCallbackDelegate _endContact = (a, b, c, _, __) => endContact(NewObject<Fixture>(a), NewObject<Fixture>(b), NewObject<Contact>(c));
            WrapWorldContactCallbackDelegate _preSolve = (a, b, c, _, __) => preSolve(NewObject<Fixture>(a), NewObject<Fixture>(b), NewObject<Contact>(c));
            WrapWorldContactCallbackDelegate _postSolve = (a, b, c, impArray, impArrayLength) => postSolve(NewObject<Fixture>(a), NewObject<Fixture>(b), NewObject<Contact>(c),
                    DllTool.ReadVector2sAndRelease(impArray, impArrayLength));
            WrapWorldContactFilterCallbackDelegate _filter = (a, b) => filterCallback(NewObject<Fixture>(a), NewObject<Fixture>(b));
            Love2dDll.wrap_love_dll_type_World_update(p, dt, velocityiterations, positioniterations,
                beginContact != null ? _beginContact : null,
                endContact != null ? _endContact : null,
                preSolve != null ? _preSolve : null,
                postSolve != null ? _postSolve : null,
                filterCallback != null ? _filter : null
                );
        }

        public delegate bool QueryBoundingBoxCallback(Fixture pfixture);

        public Vector2 GetGravity()
        {
            Love2dDll.wrap_love_dll_type_World_getGravity(p, out float x, out float y);
            return new Vector2(x, y);
        }

        public Body[] GetBodies()
        {
            Love2dDll.wrap_love_dll_type_World_getBodies(p, out var bodyList, out var bodyListLength);
            return DllTool.ReadIntPtrsWithConvertAndRelease<Body>(bodyList, bodyListLength);
        }

        public Joint[] GetJoints()
        {
            Love2dDll.wrap_love_dll_type_World_getJoints(p, out var jointList, out var jointListLength);
            var jlist = DllTool.ReadIntPtrsWithConvertAndRelease<Joint>(jointList, jointListLength);
            return Joint.JointRetainToRegularJoint(jlist);
        }

        public Contact[] GetContacts()
        {
            Love2dDll.wrap_love_dll_type_World_getContacts(p, out var contactList, out var contactListLength);
            return DllTool.ReadIntPtrsWithConvertAndRelease<Contact>(contactList, contactListLength);
        }

        public void QueryBoundingBox(float topLeftX, float topLeftY, float bottomRightX, float bottomRightY, QueryBoundingBoxCallback callback)
        {
            Love2dDll.wrap_love_dll_type_World_queryBoundingBox(p, topLeftX, topLeftY, bottomRightX, bottomRightY, (pf) => callback(NewObject<Fixture>(pf)) );
        }


        public delegate float RayCastCallback(Fixture pfixture, float x, float y, float nx, float ny, float fraction);

        public void RayCast(float x1, float y1, float x2, float y2, RayCastCallback callback)
        {
            Love2dDll.wrap_love_dll_type_World_rayCast(p, x1, y1, x2, y2, (pf, x, y, nx, ny, fraction) => callback(NewObject<Fixture>(pf), x, y, nx, ny, fraction));
        }

    }

    public class Physics : LoveObject
    {
        /// <summary>
        /// disable construct
        /// </summary>
        protected Physics() { }

        // TODO: finishe function wrap_love_dll_physics_newWorld
        // TODO: finishe function wrap_love_dll_physics_newBody
        // TODO: finishe function wrap_love_dll_physics_newFixture
        // TODO: finishe function wrap_love_dll_physics_newCircleShape
        // TODO: finishe function wrap_love_dll_physics_newRectangleShape
        // TODO: finishe function wrap_love_dll_physics_newEdgeShape
        // TODO: finishe function wrap_love_dll_physics_newDistanceJoint
        // TODO: finishe function wrap_love_dll_physics_newMouseJoint
        // TODO: finishe function wrap_love_dll_physics_newRevoluteJoint
        // TODO: finishe function wrap_love_dll_physics_newRevoluteJoint_referenceAngle
        // TODO: finishe function wrap_love_dll_physics_newPrismaticJoint
        // TODO: finishe function wrap_love_dll_physics_newPrismaticJoint_referenceAngle
        // TODO: finishe function wrap_love_dll_physics_newPulleyJoint
        // TODO: finishe function wrap_love_dll_physics_newGearJoint
        // TODO: finishe function wrap_love_dll_physics_newFrictionJoint
        // TODO: finishe function wrap_love_dll_physics_newWeldJoint
        // TODO: finishe function wrap_love_dll_physics_newWeldJoint_referenceAngle
        // TODO: finishe function wrap_love_dll_physics_newWheelJoint
        // TODO: finishe function wrap_love_dll_physics_newRopeJoint
        // TODO: finishe function wrap_love_dll_physics_newMotorJoint
        // TODO: finishe function wrap_love_dll_physics_newPolygonShape
        // TODO: finishe function wrap_love_dll_physics_newChainShape
        // TODO: finishe function wrap_love_dll_physics_open_love_physics
        // TODO: finishe function wrap_love_dll_physics_setMeter
        // TODO: finishe function wrap_love_dll_physics_getMeter
        // TODO: finishe function wrap_love_dll_physics_getDistance
    }
    public class ChainShape : Shape
    {
        /// <summary>
        /// disable construct
        /// </summary>
        protected ChainShape() { }

        public void SetNextVertex_nil()
        {
            Love2dDll.wrap_love_dll_type_ChainShape_setNextVertex_nil(p);
        }

        public void SetNextVertex(float x, float y)
        {
            Love2dDll.wrap_love_dll_type_ChainShape_setNextVertex(p, x, y);
        }

        public void SetPreviousVertex_nil()
        {
            Love2dDll.wrap_love_dll_type_ChainShape_setPreviousVertex_nil(p);
        }

        public void SetPreviousVertex(float x, float y)
        {
            Love2dDll.wrap_love_dll_type_ChainShape_setPreviousVertex(p, x, y);
        }

        public EdgeShape GetChildEdge(int index)
        {
            IntPtr out_edgeShape;
            Love2dDll.wrap_love_dll_type_ChainShape_getChildEdge(p, index, out out_edgeShape);
            return  NewObject<EdgeShape>(out_edgeShape);
        }

        public int GetVertexCount()
        {
            int out_count;
            Love2dDll.wrap_love_dll_type_ChainShape_getVertexCount(p, out out_count);
            return out_count;
        }

        public Vector2 GetPoint(int index)
        {
            Vector2 out_point;
            Love2dDll.wrap_love_dll_type_ChainShape_getPoint(p, index, out out_point);
            return out_point;
        }

        public bool GetNextVertex(out Vector2 point)
        {
            Love2dDll.wrap_love_dll_type_ChainShape_getNextVertex(p, out var hasNextVertex, out point);
            return hasNextVertex;
        }

        public bool GetPreviousVertex(out Vector2 point)
        {
            Love2dDll.wrap_love_dll_type_ChainShape_getPreviousVertex(p, out var hasPrevVertex, out point);
            return hasPrevVertex;
        }

        public Vector2[] GetPoints()
        {
            Love2dDll.wrap_love_dll_type_ChainShape_getPoints(p, out var plist, out var plistLen);
            return DllTool.ReadVector2sAndRelease(plist, plistLen);
        }
    }
    public class CircleShape : Shape
    {
        /// <summary>
        /// disable construct
        /// </summary>
        protected CircleShape() { }
        public float GetRadius()
        {
            float out_radius;
            Love2dDll.wrap_love_dll_type_CircleShape_getRadius(p, out out_radius);
            return out_radius;
        }

        public void SetRadius(float r)
        {
            Love2dDll.wrap_love_dll_type_CircleShape_setRadius(p, r);
        }

        public Vector2 GetPoint()
        {
            Vector2 out_point;
            Love2dDll.wrap_love_dll_type_CircleShape_getPoint(p, out out_point);
            return out_point;
        }

        public void SetPoint(float x, float y)
        {
            Love2dDll.wrap_love_dll_type_CircleShape_setPoint(p, x, y);
        }

    }
    public class EdgeShape : Shape
    {
        /// <summary>
        /// disable construct
        /// </summary>
        protected EdgeShape() { }
        public void SetNextVertex_nil()
        {
            Love2dDll.wrap_love_dll_type_EdgeShape_setNextVertex_nil(p);
        }

        public void SetNextVertex(float x, float y)
        {
            Love2dDll.wrap_love_dll_type_EdgeShape_setNextVertex(p, x, y);
        }

        public void SetPreviousVertex_nil()
        {
            Love2dDll.wrap_love_dll_type_EdgeShape_setPreviousVertex_nil(p);
        }

        public void SetPreviousVertex(float x, float y)
        {
            Love2dDll.wrap_love_dll_type_EdgeShape_setPreviousVertex(p, x, y);
        }

        public bool GetNextVertex(out Vector2 point)
        {
            Love2dDll.wrap_love_dll_type_EdgeShape_getNextVertex(p, out var hasNextVertex, out point);
            return hasNextVertex;
        }

        public bool GetPreviousVertex(out Vector2 point)
        {
            Love2dDll.wrap_love_dll_type_EdgeShape_getPreviousVertex(p, out var hasPrevVertex, out point);
            return hasPrevVertex;
        }

        public void GetPoints(out float x1, out float y1, out float x2, out float y2)
        {
            Love2dDll.wrap_love_dll_type_EdgeShape_getPoints(p, out x1, out y1, out x2, out y2);
        }
    }
    public class PolygonShape : Shape
    {
        /// <summary>
        /// disable construct
        /// </summary>
        protected PolygonShape() { }
        public bool Validate()
        {
            bool out_validate;
            Love2dDll.wrap_love_dll_type_PolygonShape_validate(p, out out_validate);
            return out_validate;
        }

        public Vector2[] GetPoints()
        {
            Love2dDll.wrap_love_dll_type_PolygonShape_getPoints(p, out var plist, out var plistLen);
            return DllTool.ReadVector2sAndRelease(plist, plistLen);
        }
    }
    public class DistanceJoint : Joint
    {
        /// <summary>
        /// disable construct
        /// </summary>
        protected DistanceJoint() { }
        public void SetLength(float length)
        {
            Love2dDll.wrap_love_dll_type_DistanceJoint_setLength(p, length);
        }

        public float GetLength()
        {
            float out_length;
            Love2dDll.wrap_love_dll_type_DistanceJoint_getLength(p, out out_length);
            return out_length;
        }

        public void SetFrequency(float frequency)
        {
            Love2dDll.wrap_love_dll_type_DistanceJoint_setFrequency(p, frequency);
        }

        public float GetFrequency()
        {
            float out_frequency;
            Love2dDll.wrap_love_dll_type_DistanceJoint_getFrequency(p, out out_frequency);
            return out_frequency;
        }

        public void SetDampingRatio(float dampingRatio)
        {
            Love2dDll.wrap_love_dll_type_DistanceJoint_setDampingRatio(p, dampingRatio);
        }

        public float GetDampingRatio()
        {
            float out_dampingRatio;
            Love2dDll.wrap_love_dll_type_DistanceJoint_getDampingRatio(p, out out_dampingRatio);
            return out_dampingRatio;
        }

    }
    public class FrictionJoint : Joint
    {
        /// <summary>
        /// disable construct
        /// </summary>
        protected FrictionJoint() { }
        public void SetMaxForce(float maxForce)
        {
            Love2dDll.wrap_love_dll_type_FrictionJoint_setMaxForce(p, maxForce);
        }

        public float GetMaxForce()
        {
            float out_maxForce;
            Love2dDll.wrap_love_dll_type_FrictionJoint_getMaxForce(p, out out_maxForce);
            return out_maxForce;
        }

        public void SetMaxTorque(float maxTorque)
        {
            Love2dDll.wrap_love_dll_type_FrictionJoint_setMaxTorque(p, maxTorque);
        }

        public float GetMaxTorque()
        {
            float out_maxTorque;
            Love2dDll.wrap_love_dll_type_FrictionJoint_getMaxTorque(p, out out_maxTorque);
            return out_maxTorque;
        }

    }
    public class GearJoint : Joint
    {
        /// <summary>
        /// disable construct
        /// </summary>
        protected GearJoint() { }
        public void SetRatio(float ration)
        {
            Love2dDll.wrap_love_dll_type_GearJoint_setRatio(p, ration);
        }

        public float GetRatio()
        {
            float out_ration;
            Love2dDll.wrap_love_dll_type_GearJoint_getRatio(p, out out_ration);
            return out_ration;
        }

        public void GetJoints(out Joint j1, out Joint j2)
        {
            Love2dDll.wrap_love_dll_type_GearJoint_getJoints(p, out var pj1, out var pj2);
            j1 = JointRetainToRegularJoint(NewObject<Joint>(pj1));
            j2 = JointRetainToRegularJoint(NewObject<Joint>(pj2));
        }
    }
    public class MotorJoint : Joint
    {
        /// <summary>
        /// disable construct
        /// </summary>
        protected MotorJoint() { }
        public void SetLinearOffset(float x, float y)
        {
            Love2dDll.wrap_love_dll_type_MotorJoint_setLinearOffset(p, x, y);
        }

        public void SetAngularOffset(float angularOffset)
        {
            Love2dDll.wrap_love_dll_type_MotorJoint_setAngularOffset(p, angularOffset);
        }

        public float GetAngularOffset()
        {
            float out_angularOffset;
            Love2dDll.wrap_love_dll_type_MotorJoint_getAngularOffset(p, out out_angularOffset);
            return out_angularOffset;
        }

        public void SetMaxForce(float maxForce)
        {
            Love2dDll.wrap_love_dll_type_MotorJoint_setMaxForce(p, maxForce);
        }

        public float GetMaxForce()
        {
            float out_maxForce;
            Love2dDll.wrap_love_dll_type_MotorJoint_getMaxForce(p, out out_maxForce);
            return out_maxForce;
        }

        public void SetMaxTorque(float torque)
        {
            Love2dDll.wrap_love_dll_type_MotorJoint_setMaxTorque(p, torque);
        }

        public float GetMaxTorque()
        {
            float out_torque;
            Love2dDll.wrap_love_dll_type_MotorJoint_getMaxTorque(p, out out_torque);
            return out_torque;
        }

        public void SetCorrectionFactor(float factor)
        {
            Love2dDll.wrap_love_dll_type_MotorJoint_setCorrectionFactor(p, factor);
        }

        public float GetCorrectionFactor()
        {
            float out_factor;
            Love2dDll.wrap_love_dll_type_MotorJoint_getCorrectionFactor(p, out out_factor);
            return out_factor;
        }

        public Vector2 GetLinearOffset()
        {
            Love2dDll.wrap_love_dll_type_MotorJoint_getLinearOffset(p, out float x, out float y);
            return new Vector2(x, y);
        }
    }
    public class MouseJoint : Joint
    {
        /// <summary>
        /// disable construct
        /// </summary>
        protected MouseJoint() { }
        public void SetTarget(float x, float y)
        {
            Love2dDll.wrap_love_dll_type_MouseJoint_setTarget(p, x, y);
        }

        public void SetMaxForce(float force)
        {
            Love2dDll.wrap_love_dll_type_MouseJoint_setMaxForce(p, force);
        }

        public float GetMaxForce()
        {
            float out_force;
            Love2dDll.wrap_love_dll_type_MouseJoint_getMaxForce(p, out out_force);
            return out_force;
        }

        public void SetFrequency(float frequency)
        {
            Love2dDll.wrap_love_dll_type_MouseJoint_setFrequency(p, frequency);
        }

        public float GetFrequency()
        {
            float out_frequency;
            Love2dDll.wrap_love_dll_type_MouseJoint_getFrequency(p, out out_frequency);
            return out_frequency;
        }

        public void SetDampingRatio(float ratio)
        {
            Love2dDll.wrap_love_dll_type_MouseJoint_setDampingRatio(p, ratio);
        }

        public float GetDampingRatio()
        {
            float out_ratio;
            Love2dDll.wrap_love_dll_type_MouseJoint_getDampingRatio(p, out out_ratio);
            return out_ratio;
        }


        public Vector2 GetTarget()
        {
            Love2dDll.wrap_love_dll_type_MouseJoint_getTarget(p, out float x, out float y);
            return new Vector2(x, y);
        }

    }
    public class PrismaticJoint : Joint
    {
        /// <summary>
        /// disable construct
        /// </summary>
        protected PrismaticJoint() { }
        public float GetJointTranslation()
        {
            float out_translation;
            Love2dDll.wrap_love_dll_type_PrismaticJoint_getJointTranslation(p, out out_translation);
            return out_translation;
        }

        public float GetJointSpeed()
        {
            float out_speed;
            Love2dDll.wrap_love_dll_type_PrismaticJoint_getJointSpeed(p, out out_speed);
            return out_speed;
        }

        public void SetMotorEnabled(bool ebabled)
        {
            Love2dDll.wrap_love_dll_type_PrismaticJoint_setMotorEnabled(p, ebabled);
        }

        public bool IsMotorEnabled()
        {
            bool out_enabled;
            Love2dDll.wrap_love_dll_type_PrismaticJoint_isMotorEnabled(p, out out_enabled);
            return out_enabled;
        }

        public void SetMaxMotorForce(float force)
        {
            Love2dDll.wrap_love_dll_type_PrismaticJoint_setMaxMotorForce(p, force);
        }

        public void SetMotorSpeed(float speed)
        {
            Love2dDll.wrap_love_dll_type_PrismaticJoint_setMotorSpeed(p, speed);
        }

        public float GetMotorSpeed()
        {
            float out_speed;
            Love2dDll.wrap_love_dll_type_PrismaticJoint_getMotorSpeed(p, out out_speed);
            return out_speed;
        }

        public float GetMotorForce(float inv_dt)
        {
            float out_force;
            Love2dDll.wrap_love_dll_type_PrismaticJoint_getMotorForce(p, inv_dt, out out_force);
            return out_force;
        }

        public float GetMaxMotorForce()
        {
            float out_force;
            Love2dDll.wrap_love_dll_type_PrismaticJoint_getMaxMotorForce(p, out out_force);
            return out_force;
        }

        public void SetLimitsEnabled(bool enabled)
        {
            Love2dDll.wrap_love_dll_type_PrismaticJoint_setLimitsEnabled(p, enabled);
        }

        public bool AreLimitsEnabled()
        {
            bool out_enabled;
            Love2dDll.wrap_love_dll_type_PrismaticJoint_areLimitsEnabled(p, out out_enabled);
            return out_enabled;
        }

        public void SetUpperLimit(float limit)
        {
            Love2dDll.wrap_love_dll_type_PrismaticJoint_setUpperLimit(p, limit);
        }

        public void SetLowerLimit(float limit)
        {
            Love2dDll.wrap_love_dll_type_PrismaticJoint_setLowerLimit(p, limit);
        }

        public void SetLimits(float lowerLimit, float upperLimit)
        {
            Love2dDll.wrap_love_dll_type_PrismaticJoint_setLimits(p, lowerLimit, upperLimit);
        }

        public float GetLowerLimit()
        {
            float out_limit;
            Love2dDll.wrap_love_dll_type_PrismaticJoint_getLowerLimit(p, out out_limit);
            return out_limit;
        }

        public float GetUpperLimit()
        {
            float out_limit;
            Love2dDll.wrap_love_dll_type_PrismaticJoint_getUpperLimit(p, out out_limit);
            return out_limit;
        }

        public float GetReferenceAngle()
        {
            float out_angle;
            Love2dDll.wrap_love_dll_type_PrismaticJoint_getReferenceAngle(p, out out_angle);
            return out_angle;
        }

        public void GetLimits(out float lowerLimit, out float upperLimit)
        {
            Love2dDll.wrap_love_dll_type_PrismaticJoint_getLimits(p, out lowerLimit, out upperLimit);
        }

        public void GetAxis(out float axisX, out float axisY)
        {
            Love2dDll.wrap_love_dll_type_PrismaticJoint_getAxis(p, out axisX, out axisY);
        }
    }
    public class PulleyJoint : Joint
    {
        /// <summary>
        /// disable construct
        /// </summary>
        protected PulleyJoint() { }
        public float GetLengthA()
        {
            float out_lengthA;
            Love2dDll.wrap_love_dll_type_PulleyJoint_getLengthA(p, out out_lengthA);
            return out_lengthA;
        }

        public float GetLengthB()
        {
            float out_lengthB;
            Love2dDll.wrap_love_dll_type_PulleyJoint_getLengthB(p, out out_lengthB);
            return out_lengthB;
        }

        public float GetRatio()
        {
            float out_ratio;
            Love2dDll.wrap_love_dll_type_PulleyJoint_getRatio(p, out out_ratio);
            return out_ratio;
        }

        public void GetGroundAnchors(out float x1, out float y1, out float x2, out float y2)
        {
            Love2dDll.wrap_love_dll_type_PulleyJoint_getGroundAnchors(p, out x1, out y1, out x2, out y2);
        }
    }
    public class RevoluteJoint : Joint
    {
        /// <summary>
        /// disable construct
        /// </summary>
        protected RevoluteJoint() { }
        public float GetJointAngle()
        {
            float out_angle;
            Love2dDll.wrap_love_dll_type_RevoluteJoint_getJointAngle(p, out out_angle);
            return out_angle;
        }

        public float GetJointSpeed()
        {
            float out_speed;
            Love2dDll.wrap_love_dll_type_RevoluteJoint_getJointSpeed(p, out out_speed);
            return out_speed;
        }

        public void SetMotorEnabled(bool enabled)
        {
            Love2dDll.wrap_love_dll_type_RevoluteJoint_setMotorEnabled(p, enabled);
        }

        public bool IsMotorEnabled()
        {
            bool out_enabled;
            Love2dDll.wrap_love_dll_type_RevoluteJoint_isMotorEnabled(p, out out_enabled);
            return out_enabled;
        }

        public void SetMaxMotorTorque(float torque)
        {
            Love2dDll.wrap_love_dll_type_RevoluteJoint_setMaxMotorTorque(p, torque);
        }

        public void SetMotorSpeed(float speed)
        {
            Love2dDll.wrap_love_dll_type_RevoluteJoint_setMotorSpeed(p, speed);
        }

        public float GetMotorSpeed()
        {
            float out_speed;
            Love2dDll.wrap_love_dll_type_RevoluteJoint_getMotorSpeed(p, out out_speed);
            return out_speed;
        }

        public float GetMotorTorque(float inv_dt)
        {
            float out_torque;
            Love2dDll.wrap_love_dll_type_RevoluteJoint_getMotorTorque(p, inv_dt, out out_torque);
            return out_torque;
        }

        public float GetMaxMotorTorque()
        {
            float out_torque;
            Love2dDll.wrap_love_dll_type_RevoluteJoint_getMaxMotorTorque(p, out out_torque);
            return out_torque;
        }

        public void SetLimitsEnabled(bool enabled)
        {
            Love2dDll.wrap_love_dll_type_RevoluteJoint_setLimitsEnabled(p, enabled);
        }

        public bool AreLimitsEnabled()
        {
            bool out_enabled;
            Love2dDll.wrap_love_dll_type_RevoluteJoint_areLimitsEnabled(p, out out_enabled);
            return out_enabled;
        }

        public void SetUpperLimit(float limit)
        {
            Love2dDll.wrap_love_dll_type_RevoluteJoint_setUpperLimit(p, limit);
        }

        public void SetLowerLimit(float limit)
        {
            Love2dDll.wrap_love_dll_type_RevoluteJoint_setLowerLimit(p, limit);
        }

        public void SetLimits(float lowerLimit, float upperLimit)
        {
            Love2dDll.wrap_love_dll_type_RevoluteJoint_setLimits(p, lowerLimit, upperLimit);
        }

        public float GetLowerLimit()
        {
            float out_limit;
            Love2dDll.wrap_love_dll_type_RevoluteJoint_getLowerLimit(p, out out_limit);
            return out_limit;
        }

        public float GetUpperLimit()
        {
            float out_limit;
            Love2dDll.wrap_love_dll_type_RevoluteJoint_getUpperLimit(p, out out_limit);
            return out_limit;
        }

        public float GetReferenceAngle()
        {
            float out_angle;
            Love2dDll.wrap_love_dll_type_RevoluteJoint_getReferenceAngle(p, out out_angle);
            return out_angle;
        }

        public void GetLimits(out float lowerLimit, out float upperLimit)
        {
            Love2dDll.wrap_love_dll_type_RevoluteJoint_getLimits(p, out lowerLimit, out upperLimit);
        }
    }


    public class RopeJoint : Joint
    {
        /// <summary>
        /// disable construct
        /// </summary>
        protected RopeJoint() { }

        public float GetMaxLength()
        {
            Love2dDll.wrap_love_dll_type_RopeJoint_getMaxLength(p, out float maxLength);
            return maxLength;
        }

        public void SetMaxLength(float maxLength)
        {
            Love2dDll.wrap_love_dll_type_RopeJoint_setMaxLength(p, maxLength);
        }

    }
    public class WeldJoint : Joint
    {
        /// <summary>
        /// disable construct
        /// </summary>
        protected WeldJoint() { }
        public void SetFrequency(float frequency)
        {
            Love2dDll.wrap_love_dll_type_WeldJoint_setFrequency(p, frequency);
        }

        public float GetFrequency()
        {
            float out_frequency;
            Love2dDll.wrap_love_dll_type_WeldJoint_getFrequency(p, out out_frequency);
            return out_frequency;
        }

        public void SetDampingRatio(float ratio)
        {
            Love2dDll.wrap_love_dll_type_WeldJoint_setDampingRatio(p, ratio);
        }

        public float GetDampingRatio()
        {
            float out_ratio;
            Love2dDll.wrap_love_dll_type_WeldJoint_getDampingRatio(p, out out_ratio);
            return out_ratio;
        }

        public float GetReferenceAngle()
        {
            float out_angle;
            Love2dDll.wrap_love_dll_type_WeldJoint_getReferenceAngle(p, out out_angle);
            return out_angle;
        }

    }
    public class WheelJoint : Joint
    {
        /// <summary>
        /// disable construct
        /// </summary>
        protected WheelJoint() { }
        public float GetJointTranslation()
        {
            float out_translation;
            Love2dDll.wrap_love_dll_type_WheelJoint_getJointTranslation(p, out out_translation);
            return out_translation;
        }

        public float GetJointSpeed()
        {
            float out_speed;
            Love2dDll.wrap_love_dll_type_WheelJoint_getJointSpeed(p, out out_speed);
            return out_speed;
        }

        public void SetMotorEnabled(bool enabled)
        {
            Love2dDll.wrap_love_dll_type_WheelJoint_setMotorEnabled(p, enabled);
        }

        public bool IsMotorEnabled()
        {
            bool out_enabled;
            Love2dDll.wrap_love_dll_type_WheelJoint_isMotorEnabled(p, out out_enabled);
            return out_enabled;
        }

        public void SetMotorSpeed(float speed)
        {
            Love2dDll.wrap_love_dll_type_WheelJoint_setMotorSpeed(p, speed);
        }

        public float GetMotorSpeed()
        {
            float out_speed;
            Love2dDll.wrap_love_dll_type_WheelJoint_getMotorSpeed(p, out out_speed);
            return out_speed;
        }

        public void SetMaxMotorTorque(float torque)
        {
            Love2dDll.wrap_love_dll_type_WheelJoint_setMaxMotorTorque(p, torque);
        }

        public float GetMaxMotorTorque()
        {
            float out_torque;
            Love2dDll.wrap_love_dll_type_WheelJoint_getMaxMotorTorque(p, out out_torque);
            return out_torque;
        }

        public float GetMotorTorque(float inv_dt)
        {
            float out_torque;
            Love2dDll.wrap_love_dll_type_WheelJoint_getMotorTorque(p, inv_dt, out out_torque);
            return out_torque;
        }

        public void SetSpringFrequency(float frequency)
        {
            Love2dDll.wrap_love_dll_type_WheelJoint_setSpringFrequency(p, frequency);
        }

        public float GetSpringFrequency()
        {
            float out_frequency;
            Love2dDll.wrap_love_dll_type_WheelJoint_getSpringFrequency(p, out out_frequency);
            return out_frequency;
        }

        public void SetSpringDampingRatio(float ratio)
        {
            Love2dDll.wrap_love_dll_type_WheelJoint_setSpringDampingRatio(p, ratio);
        }

        public float GetSpringDampingRatio()
        {
            float out_ratio;
            Love2dDll.wrap_love_dll_type_WheelJoint_getSpringDampingRatio(p, out out_ratio);
            return out_ratio;
        }

        public void GetAxis(out float axisX, out float axisY)
        {
            Love2dDll.wrap_love_dll_type_WheelJoint_getAxis(p, out axisX, out axisY);
        }
    }






}
#endregion


