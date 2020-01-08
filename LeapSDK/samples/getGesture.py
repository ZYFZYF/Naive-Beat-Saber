import sys
sys.path.insert(0, "../lib")
import Leap, thread, time
import json
import socket
from Leap import CircleGesture, KeyTapGesture, ScreenTapGesture, SwipeGesture


class SampleListener(Leap.Listener):
    finger_names = ['Thumb', 'Index', 'Middle', 'Ring', 'Pinky']
    bone_names = ['Metacarpal', 'Proximal', 'Intermediate', 'Distal']
    state_names = ['STATE_INVALID', 'STATE_START', 'STATE_UPDATE', 'STATE_END']

    def on_init(self, controller):
        print "Initialized"

    def on_connect(self, controller):
        print "Connected"

    def on_disconnect(self, controller):
        # Note: not dispatched when running in a debugger.
        print "Disconnected"

    def on_exit(self, controller):
        print "Exited"

    def on_frame(self, controller):
        # Get the most recent frame and report some basic information
        frame = controller.frame()

        # print "Frame id: %d, timestamp: %d, hands: %d, fingers: %d, tools: %d, gestures: %d" % (
        #       frame.id, frame.timestamp, len(frame.hands), len(frame.fingers), len(frame.tools), len(frame.gestures()))

        # Get hands
        msg = {}
        for hand in frame.hands:

            handType = "Left hand" if hand.is_left else "Right hand"
            siglehandmsg = {}
            # 300
            # print abs(hand.palm_velocity.y)
            # Get fingers
            for finger in hand.fingers:

                if self.finger_names[finger.type] == 'Index':
                    # print "    %s finger, id: %d, length: %fmm, width: %fmm" % (
                    # self.finger_names[finger.type],
                    # finger.id,
                    # finger.length,
                    # finger.width)
                    mybone = [1,3]
                    # Get bones
                    for b in mybone:
                        bone = finger.bone(b)
                        siglehandmsg[self.bone_names[bone.type]] = str(bone.next_joint.x)+" "+str(bone.next_joint.y)+" "+str(-bone.next_joint.z)
                        # print "      Bone: %s, start: %s, end: %s, direction: %s" % (
                            # self.bone_names[bone.type],
                            # bone.prev_joint,
                            # bone.next_joint,
                            # bone.direction)
            msg[handType] = json.dumps(siglehandmsg)
        if not msg == {}:
            json_str = json.dumps(msg)
            print(json_str)
            address = ('192.168.43.1', 2233)
            s = socket.socket(socket.AF_INET, socket.SOCK_DGRAM)
            s.sendto(bytes(json_str), address)
            s.close()

        if not (frame.hands.is_empty and frame.gestures().is_empty):
            print ""

    def state_string(self, state):
        if state == Leap.Gesture.STATE_START:
            return "STATE_START"

        if state == Leap.Gesture.STATE_UPDATE:
            return "STATE_UPDATE"

        if state == Leap.Gesture.STATE_STOP:
            return "STATE_STOP"

        if state == Leap.Gesture.STATE_INVALID:
            return "STATE_INVALID"

def main():
    # Create a sample listener and controller
    listener = SampleListener()
    controller = Leap.Controller()

    # Have the sample listener receive events from the controller
    controller.add_listener(listener)

    # Keep this process running until Enter is pressed
    print "Press Enter to quit..."
    try:
        sys.stdin.readline()
    except KeyboardInterrupt:
        pass
    finally:
        # Remove the sample listener when done
        controller.remove_listener(listener)


if __name__ == "__main__":
    main()


