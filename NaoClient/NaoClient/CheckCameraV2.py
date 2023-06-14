import cv2

# Check camera initialization
cap = cv2.VideoCapture(0)
if not cap.isOpened():
    print("Failed to open the camera")
    exit(1)

# Check camera properties
width = cap.get(cv2.CAP_PROP_FRAME_WIDTH)
height = cap.get(cv2.CAP_PROP_FRAME_HEIGHT)
fps = cap.get(cv2.CAP_PROP_FPS)
print("Camera properties:")
print("Resolution: {}x{}".format(int(width), int(height)))
print("FPS: {}".format(fps))

# Capture and display frames
while True:
    # Capture a frame from the camera
    ret, frame = cap.read()

    # Check if the frame is valid
    if ret and frame is not None and frame.shape[0] > 0 and frame.shape[1] > 0:
        # Display the frame
        cv2.imshow("Camera", frame)
    else:
        print("Invalid frame or empty frame received")

    # Exit the loop on 'q' key press
    if cv2.waitKey(1) & 0xFF == ord("q"):
        break

# Release the camera and close windows
cap.release()
cv2.destroyAllWindows()
