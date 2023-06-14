import cv2

# Create a VideoCapture object for the robot's camera
cap = cv2.VideoCapture(0)

# Set the camera resolution (optional)
cap.set(cv2.CAP_PROP_FRAME_WIDTH, 640)
cap.set(cv2.CAP_PROP_FRAME_HEIGHT, 480)

# Capture a frame from the camera
ret, frame = cap.read()

# Check if the frame is valid
if not ret or frame is None or frame.shape[0] == 0 or frame.shape[1] == 0:
    print("Invalid frame or empty frame received")
else:
    # Calculate the size of the image in bytes
    img_bytes = frame.tobytes()
    img_size_bytes = len(img_bytes)
    print("Image size:", img_size_bytes, "bytes")

# Release the camera
cap.release()
