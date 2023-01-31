# Các thư viện cần thiết

#socket để truyền dữ liệu qua giao thức UDP
import socket

#Opencv là thư viện giúp xử lí ảnh
import cv2

#Cvzone là thư viện giúp nhận diện tay và khớp
from cvzone.HandTrackingModule import HandDetector

#Chiều cao và rộng cửa sổ khi bật camera
height, width = 720, 1280

#Bật webcam và set chiều cao rộng
cap = cv2.VideoCapture(0)
cap.set(3,width)
cap.set(4,height)

# hand detector
# Dùng thư viện cvzone nhận diện tay với tối đa 1 tay, độ tự tin 0.8
#######  *Độ tự tin: nếu chỉ số quá thấp, những hình dáng không phải tay có thể bị nhận nhầm, nếu chỉ số quá cao, điều kiện ánh sáng kém sẽ không thể nhận diện được
detector = HandDetector(maxHands=1, detectionCon=0.8)


#communicate
# khởi tạo socket
sock = socket.socket(socket.AF_INET, socket.SOCK_DGRAM)

#địa chỉ localhost port 5052
serverAddrandPort = ("127.0.0.1", 5052)

while True:
    #get the video from cam
    succes,img = cap.read()
    
    #hand detect
    hands, img = detector.findHands(img)
    cv2.imshow("image", img)
    cv2.waitKey(1)

    # gán toạ độ vào data và truyền cho unity+
    data = []
    #21 landmark value
    # lấy ngón điểm 0 và 12 sau đó truyền đi
    if hands:
        hand = hands[0]
        lmList = hand['lmList']
        data.extend([lmList[0][0], height - lmList[0][1]])
        data.extend([lmList[12][0], height - lmList[12][1]])
        #gửi qua giao thức UDP 
        sock.sendto(str.encode(str(data)), serverAddrandPort)
        print(data)

