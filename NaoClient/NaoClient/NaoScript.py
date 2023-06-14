import cv2
import socket

# Define the server address and port
server_address = '10.0.0.154'  # Replace with the IP address of your server
server_port = 12345

# Create a socket and connect to the server
sock = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
try:
    sock.connect((server_address, server_port))
    print("Connected to the server:", server_address, "on port:", server_port)

    # Create a VideoCapture object for the robot's camera
    cap = cv2.VideoCapture(0)

    # Set the camera resolution (optional)
    cap.set(cv2.CAP_PROP_FRAME_WIDTH, 640)
    cap.set(cv2.CAP_PROP_FRAME_HEIGHT, 480)

    # Capture and send frames
    while True:
        # Capture a frame from the camera
        ret, frame = cap.read()

        # Check if the frame is valid
        if not ret or frame is None or frame.shape[0] == 0 or frame.shape[1] == 0:
            print("Invalid frame or empty frame received")
            continue

        try:
            # Encode the frame as a JPEG image
            _, img_encoded = cv2.imencode('.jpg', frame)
        except cv2.error as e:
            print("Error encoding the frame:", e)
            continue

        # Send the encoded frame to the server
        try:
            sock.sendall(img_encoded.tobytes())
        except Exception as e:
            print("Error sending the frame:", e)
            break

finally:
    # Release the camera and close the socket
    cap.release()
    sock.close()
