using System;
using System.Text;
using UnityEngine;
using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Messages;

public class ManagePos : MonoBehaviour
{
    MqttClient client;
    float x = 0;
    float y = 0;

    public GameObject tObject;

    void Start()
    {
        // Connect to HiveMQ broker using TCP port 1883
        client = new MqttClient("broker.hivemq.com");

        client.MqttMsgPublishReceived += client_MqttMsgPublishReceived;

        try
        {
            client.Connect(Guid.NewGuid().ToString()); // Use a unique client ID
            // Subscribe to the topic "/nilVR" with QoS level 0
            client.Subscribe(new string[] { "/nilVR" }, new byte[] { MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE });
        }
        catch (Exception ex)
        {
            Debug.LogError("Could not connect to MQTT broker: " + ex.Message);
        }
    }

    public void client_MqttMsgPublishReceived(object sender, MqttMsgPublishEventArgs e)
    {
        string message = Encoding.UTF8.GetString(e.Message);
        string[] coords = message.Split(',');

        if (coords.Length == 2 &&
            float.TryParse(coords[0], out x) &&
            float.TryParse(coords[1], out y))
        {
            Debug.Log("X: " + x + ", Y: " + y);
            // updatePosition(tObject, x, y);
        }
        else
        {
            Debug.LogWarning("Invalid message received: " + message);
        }
    }

    public void updatePosition(GameObject tObject, float x, float y)
    {
        if (tObject != null)
        {
            // Update the position based on the received coordinates
            tObject.transform.position = new Vector3(x, y, tObject.transform.position.z);
            Debug.Log("Position updated to X: " + x + ", Y: " + y);
        }
        else
        {
            Debug.LogError("Target object not assigned.");
        }
    }
}