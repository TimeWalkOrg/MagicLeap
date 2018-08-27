using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

    public class NatureTilesDemoGUI : MonoBehaviour
    {


        public Button goUpAnchor_Button;
        public Button goDownAnchor_Button;
        public Text AnchorPosText;

       

        public GameObject cinematicCameraObject;
        CinematicCamera cineCam;
        int currentAnchor;

        void Start()
        {
      
            if (cinematicCameraObject != null)
            
                cineCam = cinematicCameraObject.GetComponent<CinematicCamera>();
              

       
        

            goDownAnchor_Button.onClick.AddListener(delegate { CallAnchorChangeDown(); });
            goUpAnchor_Button.onClick.AddListener(delegate { CallAnchorChangeUp(); });
        }
       
        void CallAnchorChangeDown()
        {
            currentAnchor -= 1;
            if (currentAnchor < 0)
                currentAnchor = cineCam.cameraAnchors.Count-1;

            cineCam.SwitchToAnchor(currentAnchor);
         
        }
        void CallAnchorChangeUp()
        {
            currentAnchor += 1;
            if (currentAnchor > cineCam.cameraAnchors.Count-1)
                currentAnchor = 0;

            cineCam.SwitchToAnchor(currentAnchor);
        }
        
       

      
    }
