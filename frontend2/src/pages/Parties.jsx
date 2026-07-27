import { useEffect, useState } from "react";

import {
    getParties,
    addParty,
    deleteParty
} from "../services/partyService";

function Parties() {

    const [parties, setParties] = useState([]);

    const [partyName, setPartyName] = useState("");

    const [phone, setPhone] = useState("");

    const [address, setAddress] = useState("");

    const [gstNumber, setGstNumber] = useState("");

    async function loadParties() {

        const data = await getParties();

        setParties(data);

    }

    async function handleAddParty() {

        if (!partyName) {

            alert("Enter Party Name");

            return;

        }

        if (!phone) {

            alert("Enter Phone Number");

            return;

        }

        const party = {

            partyName: partyName,

            phone: phone,

            address: address,

            gstin: gstNumber

        };

        await addParty(party);

        await loadParties();

        setPartyName("");

        setPhone("");

        setAddress("");

        setGstNumber("");

    }

    async function handleDelete(id) {

        await deleteParty(id);

        await loadParties();

    }

    useEffect(function () {

        loadParties();

    }, []);

    return (

        <div className="container">

            <h2>Parties</h2>

            <hr />

            <h3>Add Party</h3>

            <input
                type="text"
                placeholder="Party Name"
                value={partyName}
                onChange={(e) => setPartyName(e.target.value)}
            />

            <br /><br />

            <input
                type="text"
                placeholder="Phone Number"
                value={phone}
                onChange={(e) => setPhone(e.target.value)}
            />

            <br /><br />

            <input
                type="text"
                placeholder="Address"
                value={address}
                onChange={(e) => setAddress(e.target.value)}
            />

            <br /><br />

            <input
                type="text"
                placeholder="GST Number"
                value={gstNumber}
                onChange={(e) => setGstNumber(e.target.value)}
            />

            <br /><br />

            <button onClick={handleAddParty}>

                Add Party

            </button>

            <hr />

            <h3>Party List</h3>

            <table>

                <thead>

                    <tr>

                        <th>Party Name</th>

                        <th>Phone</th>

                        <th>Address</th>

                        <th>GST Number</th>

                        <th>Action</th>

                    </tr>

                </thead>

                <tbody>

                    {parties.map(function (party) {

                        return (

                            <tr key={party.id}>

                                <td>{party.partyName}</td>

                                <td>{party.phone}</td>

                                <td>{party.address}</td>

                                <td>{party.gstin}</td>

                                <td>

                                    <button
                                        onClick={function () {

                                            handleDelete(party.id);

                                        }}
                                    >

                                        Delete

                                    </button>

                                </td>

                            </tr>

                        );

                    })}

                </tbody>

            </table>

        </div>

    );

}

export default Parties;