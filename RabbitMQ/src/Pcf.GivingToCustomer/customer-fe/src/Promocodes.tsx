import { HubConnection, HubConnectionBuilder } from "@microsoft/signalr";
import { useEffect, useState } from "react";

interface PromocodeDto {
  code: string | null;
  serviceInfo: string | null;
  beginDate: string;
  endDate: string;
  partnerId: string;
  partnerManagerId: string | null;
  preferenceId: string;
}

export default function PromocodesComponent() {
  const [promoCodes, setPromoCodes] = useState<PromocodeDto[]>([]);
  const [connection, setConnection] = useState<HubConnection | null>(null);

  useEffect(() => {
    const newConnection = new HubConnectionBuilder()
      .withUrl("https://localhost:8093/promocodes", { withCredentials: false })
      .withAutomaticReconnect()
      .build();

    setConnection(newConnection);
    newConnection.start();

    return () => {
      if (newConnection) {
        newConnection.stop();
      }
    };
  }, []);

  useEffect(() => {
    if (connection) {
      connection.on("PromocodeReceived", (newCode: PromocodeDto) => {
        setPromoCodes((prev) => [...prev, newCode]);
      });
    }
  }, [connection]);

  return (
    <div>
      {promoCodes.map((code, index) => (
        <div key={index}>
          <p>
            <strong>Code:</strong> {code.code}
          </p>
          <p>
            <strong>Service Info:</strong> {code.serviceInfo}
          </p>
          <p>
            <strong>Begin Date:</strong>{" "}
            {new Date(code.beginDate).toLocaleDateString()}
          </p>
          <p>
            <strong>End Date:</strong>{" "}
            {new Date(code.endDate).toLocaleDateString()}
          </p>
          <p>
            <strong>Partner ID:</strong> {code.partnerId}
          </p>
          <p>
            <strong>Partner Manager ID:</strong>{" "}
            {code.partnerManagerId || "N/A"}
          </p>
          <p>
            <strong>Preference ID:</strong> {code.preferenceId}
          </p>
          <hr />
        </div>
      ))}
    </div>
  );
}
