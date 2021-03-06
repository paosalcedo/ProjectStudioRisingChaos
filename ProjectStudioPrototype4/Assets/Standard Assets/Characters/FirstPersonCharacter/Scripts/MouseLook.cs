using System;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

namespace UnityStandardAssets.Characters.FirstPerson
{
    [Serializable]
    public class MouseLook
    {
        public float XSensitivity = 1f;
        public float YSensitivity = 1f;
        public bool clampVerticalRotation = true;
        public float MinimumX = -90F;
        public float MaximumX = 90F;
        public bool smooth;
        public float smoothTime = 5f;
        public bool lockCursor = true;

        private bool isPlayer_1 = false;
        private Quaternion m_CharacterTargetRot;
        private Quaternion m_CameraTargetRot;
        private bool m_cursorIsLocked = true;

        public void AssignPlayers(int _playerNum){
            if(_playerNum == 0){
                isPlayer_1 = true;
            } else {
                isPlayer_1 = false;
            }
        }
        public void HideCursor(){
            m_cursorIsLocked = true;
        }
        public void Init(Transform character, Transform camera)
        {
            m_CharacterTargetRot = character.localRotation;
            m_CameraTargetRot = camera.localRotation;
            m_cursorIsLocked = true;
        }


        public void LookRotation(Transform character, Transform camera)
        {
            // float yRot = isNotPlayer_1 ? CrossPlatformInputManager.GetAxis("P2_Mouse X") * XSensitivity : CrossPlatformInputManager.GetAxis("Mouse X") * XSensitivity;
            // float xRot = isNotPlayer_1 ? CrossPlatformInputManager.GetAxis("P2_Mouse Y") * YSensitivity : CrossPlatformInputManager.GetAxis("Mouse Y") * YSensitivity;
//            float yRot = isPlayer_1 ? CrossPlatformInputManager.GetAxis("Mouse X") * XSensitivity : CrossPlatformInputManager.GetAxis("P2_Mouse X") * XSensitivity;
//            float xRot = isPlayer_1 ? CrossPlatformInputManager.GetAxis("Mouse Y") * YSensitivity : CrossPlatformInputManager.GetAxis("P2_Mouse Y") * YSensitivity;
            float yRot = isPlayer_1 ? CrossPlatformInputManager.GetAxis("Mouse X") * XSensitivity : CrossPlatformInputManager.GetAxis("Mouse X") * XSensitivity;
            float xRot = isPlayer_1 ? CrossPlatformInputManager.GetAxis("Mouse Y") * YSensitivity : CrossPlatformInputManager.GetAxis("Mouse Y") * YSensitivity;
            Vector2 stick = new Vector2(xRot, yRot);
            if (stick.magnitude <= .25){
                stick = Vector2.zero;
            }
            m_CharacterTargetRot *= Quaternion.Euler (0f, stick.y, 0f);
            m_CameraTargetRot *= Quaternion.Euler (-stick.x, 0f, 0f);

            if(clampVerticalRotation)
                m_CameraTargetRot = ClampRotationAroundXAxis (m_CameraTargetRot);

            if(smooth)
            {
                character.localRotation = Quaternion.Slerp (character.localRotation, m_CharacterTargetRot,
                    smoothTime * Time.deltaTime);
                camera.localRotation = Quaternion.Slerp (camera.localRotation, m_CameraTargetRot,
                    smoothTime * Time.deltaTime);
            }
            else
            {
                character.localRotation = m_CharacterTargetRot;
                camera.localRotation = m_CameraTargetRot;
            }

            UpdateCursorLock();
        }

        public void SetCursorLock(bool value)
        {
            lockCursor = value;
            if(!lockCursor)
            {//we force unlock the cursor if the user disable the cursor locking helper
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
        }

        public void UpdateCursorLock()
        {
            //if the user set "lockCursor" we check & properly lock the cursos
            if (lockCursor)
                InternalLockUpdate();
        }

        private void InternalLockUpdate()
        {
            if(Input.GetKeyUp(KeyCode.Escape))
            {
                m_cursorIsLocked = false;
            }
            else if(Input.GetMouseButtonUp(0))
            {
                m_cursorIsLocked = true;
            }

            if (m_cursorIsLocked)
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
            else if (!m_cursorIsLocked)
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
        }

        Quaternion ClampRotationAroundXAxis(Quaternion q)
        {
            q.x /= q.w;
            q.y /= q.w;
            q.z /= q.w;
            q.w = 1.0f;

            float angleX = 2.0f * Mathf.Rad2Deg * Mathf.Atan (q.x);

            angleX = Mathf.Clamp (angleX, MinimumX, MaximumX);

            q.x = Mathf.Tan (0.5f * Mathf.Deg2Rad * angleX);

            return q;
        }

    }
}
