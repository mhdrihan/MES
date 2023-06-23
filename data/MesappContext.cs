using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace MES.data;

public partial class MesappContext : DbContext
{
    public MesappContext()
    {
    }

    public MesappContext(DbContextOptions<MesappContext> options)
        : base(options)
    {
    }

    public virtual DbSet<TableMasterBatch> TableMasterBatches { get; set; }

    public virtual DbSet<TableMasterLine> TableMasterLines { get; set; }

    public virtual DbSet<TableMasterMaterial> TableMasterMaterials { get; set; }

    public virtual DbSet<TableMasterOrder> TableMasterOrders { get; set; }

    public virtual DbSet<TableMasterProcess> TableMasterProcesses { get; set; }

    public virtual DbSet<TableMasterProcessBy> TableMasterProcessBies { get; set; }

    public virtual DbSet<TableMasterRefference> TableMasterRefferences { get; set; }

    public virtual DbSet<TableMasterSerialCounter> TableMasterSerialCounters { get; set; }

    public virtual DbSet<TableMasterStation> TableMasterStations { get; set; }

    public virtual DbSet<TableMasterStatusOrder> TableMasterStatusOrders { get; set; }

    public virtual DbSet<TableMasterStatusResult> TableMasterStatusResults { get; set; }

    public virtual DbSet<TableMasterStatusRunning> TableMasterStatusRunnings { get; set; }

    public virtual DbSet<TableMasterUser> TableMasterUsers { get; set; }

    public virtual DbSet<TableMasterUserLevel> TableMasterUserLevels { get; set; }

    public virtual DbSet<TableMasterWorkplan> TableMasterWorkplans { get; set; }

    public virtual DbSet<TableRunTraceability> TableRunTraceabilities { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=DESKTOP-IJ50O8H;Database=MESAPP;Trusted_Connection=True;TrustServerCertificate=true;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TableMasterBatch>(entity =>
        {
            entity.HasKey(e => e.BatchId);

            entity.ToTable("Table_Master_Batch");

            entity.Property(e => e.BatchId)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Batch_ID");
            entity.Property(e => e.RefferenceName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Refference_Name");
            entity.Property(e => e.WorkOrder)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Work_Order");
        });

        modelBuilder.Entity<TableMasterLine>(entity =>
        {
            entity.HasKey(e => e.LineId);

            entity.ToTable("Table_Master_Line");

            entity.Property(e => e.LineId)
                .ValueGeneratedNever()
                .HasColumnName("Line_ID");
            entity.Property(e => e.LastModify)
                .HasColumnType("datetime")
                .HasColumnName("Last_Modify");
            entity.Property(e => e.LineDescription)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Line_Description");
            entity.Property(e => e.LineLocation)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Line_Location");
            entity.Property(e => e.LineName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Line_Name");
            entity.Property(e => e.TransactBy)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Transact_By");
        });

        modelBuilder.Entity<TableMasterMaterial>(entity =>
        {
            entity.HasKey(e => e.MaterialId);

            entity.ToTable("Table_Master_Material");

            entity.Property(e => e.MaterialId)
                .ValueGeneratedNever()
                .HasColumnName("Material_ID");
            entity.Property(e => e.LastModify)
                .HasColumnType("datetime")
                .HasColumnName("Last_Modify");
            entity.Property(e => e.MaterialName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Material_Name");
            entity.Property(e => e.Part1)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Part_1");
            entity.Property(e => e.Part10)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Part_10");
            entity.Property(e => e.Part11)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Part_11");
            entity.Property(e => e.Part12)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Part_12");
            entity.Property(e => e.Part13)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Part_13");
            entity.Property(e => e.Part14)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Part_14");
            entity.Property(e => e.Part15)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Part_15");
            entity.Property(e => e.Part16)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Part_16");
            entity.Property(e => e.Part17)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Part_17");
            entity.Property(e => e.Part18)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Part_18");
            entity.Property(e => e.Part19)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Part_19");
            entity.Property(e => e.Part2)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Part_2");
            entity.Property(e => e.Part20)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Part_20");
            entity.Property(e => e.Part21)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Part_21");
            entity.Property(e => e.Part22)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Part_22");
            entity.Property(e => e.Part23)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Part_23");
            entity.Property(e => e.Part24)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Part_24");
            entity.Property(e => e.Part25)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Part_25");
            entity.Property(e => e.Part26)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Part_26");
            entity.Property(e => e.Part27)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Part_27");
            entity.Property(e => e.Part28)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Part_28");
            entity.Property(e => e.Part29)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Part_29");
            entity.Property(e => e.Part3)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Part_3");
            entity.Property(e => e.Part30)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Part_30");
            entity.Property(e => e.Part31)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Part_31");
            entity.Property(e => e.Part32)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Part_32");
            entity.Property(e => e.Part33)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Part_33");
            entity.Property(e => e.Part34)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Part_34");
            entity.Property(e => e.Part35)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Part_35");
            entity.Property(e => e.Part36)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Part_36");
            entity.Property(e => e.Part37)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Part_37");
            entity.Property(e => e.Part38)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Part_38");
            entity.Property(e => e.Part39)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Part_39");
            entity.Property(e => e.Part4)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Part_4");
            entity.Property(e => e.Part40)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Part_40");
            entity.Property(e => e.Part41)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Part_41");
            entity.Property(e => e.Part42)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Part_42");
            entity.Property(e => e.Part43)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Part_43");
            entity.Property(e => e.Part44)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Part_44");
            entity.Property(e => e.Part45)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Part_45");
            entity.Property(e => e.Part46)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Part_46");
            entity.Property(e => e.Part47)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Part_47");
            entity.Property(e => e.Part48)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Part_48");
            entity.Property(e => e.Part49)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Part_49");
            entity.Property(e => e.Part5)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Part_5");
            entity.Property(e => e.Part50)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Part_50");
            entity.Property(e => e.Part6)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Part_6");
            entity.Property(e => e.Part7)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Part_7");
            entity.Property(e => e.Part8)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Part_8");
            entity.Property(e => e.Part9)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Part_9");
            entity.Property(e => e.PartQty).HasColumnName("Part_Qty");
            entity.Property(e => e.RefferenceName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Refference_Name");
            entity.Property(e => e.RevisionNumber).HasColumnName("Revision_Number");
            entity.Property(e => e.StationId).HasColumnName("Station_ID");
            entity.Property(e => e.TransactBy)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Transact_By");
        });

        modelBuilder.Entity<TableMasterOrder>(entity =>
        {
            entity.HasKey(e => e.WorkOrder).HasName("PK_Table_Master_Launching");

            entity.ToTable("Table_Master_Order");

            entity.Property(e => e.WorkOrder)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Work_Order");
            entity.Property(e => e.DateComplete)
                .HasColumnType("datetime")
                .HasColumnName("Date_Complete");
            entity.Property(e => e.DateOrder)
                .HasColumnType("datetime")
                .HasColumnName("Date_Order");
            entity.Property(e => e.PriorityWo).HasColumnName("Priority_WO");
            entity.Property(e => e.QtyLaunching)
                .HasDefaultValueSql("((0))")
                .HasColumnName("Qty_Launching");
            entity.Property(e => e.QtyOrder).HasColumnName("Qty_Order");
            entity.Property(e => e.RefferenceName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Refference_Name");
            entity.Property(e => e.StationId)
                .HasDefaultValueSql("((0))")
                .HasColumnName("Station_ID");
            entity.Property(e => e.StationSuffix)
                .HasDefaultValueSql("((0))")
                .HasColumnName("Station_Suffix");
            entity.Property(e => e.StatusOrder)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Status_Order");
            entity.Property(e => e.TransactBy)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Transact_By");
            entity.Property(e => e.Username)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.WoComment)
                .HasMaxLength(300)
                .IsUnicode(false)
                .HasColumnName("WO_Comment");
            entity.Property(e => e.WorkPlan)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Work_Plan");
        });

        modelBuilder.Entity<TableMasterProcess>(entity =>
        {
            entity.HasKey(e => e.ProcessId);

            entity.ToTable("Table_Master_Process");

            entity.Property(e => e.ProcessId)
                .ValueGeneratedNever()
                .HasColumnName("Process_ID");
            entity.Property(e => e.LastModify)
                .HasColumnType("datetime")
                .HasColumnName("Last_Modify");
            entity.Property(e => e.ProcessBy)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Process_By");
            entity.Property(e => e.ProcessDescription)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Process_Description");
            entity.Property(e => e.ProcessName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Process_Name");
            entity.Property(e => e.TransactBy)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Transact_By");
        });

        modelBuilder.Entity<TableMasterProcessBy>(entity =>
        {
            entity.HasKey(e => new { e.Id, e.ProcessBy });

            entity.ToTable("Table_Master_Process_By");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.ProcessBy)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Process_By");
        });

        modelBuilder.Entity<TableMasterRefference>(entity =>
        {
            entity.HasKey(e => e.RefferenceId);

            entity.ToTable("Table_Master_Refference");

            entity.Property(e => e.RefferenceId)
                .ValueGeneratedNever()
                .HasColumnName("Refference_ID");
            entity.Property(e => e.LastModify)
                .HasColumnType("datetime")
                .HasColumnName("Last_Modify");
            entity.Property(e => e.RefferenceDescription)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Refference_Description");
            entity.Property(e => e.RefferenceName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Refference_Name");
            entity.Property(e => e.TransactBy)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Transact_By");
        });

        modelBuilder.Entity<TableMasterSerialCounter>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("Table_Master_Serial_Counter");

            entity.Property(e => e.CounterCode).HasColumnName("Counter_Code");
            entity.Property(e => e.StationId).HasColumnName("Station_ID");
            entity.Property(e => e.StationSuffix).HasColumnName("Station_Suffix");
            entity.Property(e => e.WeekCode).HasColumnName("Week_Code");
            entity.Property(e => e.YearCode).HasColumnName("Year_Code");
        });

        modelBuilder.Entity<TableMasterStation>(entity =>
        {
            entity.HasKey(e => new { e.StationId, e.StationSuffix });

            entity.ToTable("Table_Master_Station");

            entity.Property(e => e.StationId).HasColumnName("Station_ID");
            entity.Property(e => e.StationSuffix).HasColumnName("Station_Suffix");
            entity.Property(e => e.LastModify)
                .HasColumnType("datetime")
                .HasColumnName("Last_Modify");
            entity.Property(e => e.LineId).HasColumnName("Line_ID");
            entity.Property(e => e.ProcessId).HasColumnName("Process_ID");
            entity.Property(e => e.StationName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Station_Name");
            entity.Property(e => e.TargetOutput).HasColumnName("Target_Output");
            entity.Property(e => e.TargetYield).HasColumnName("Target_Yield");
            entity.Property(e => e.TesterName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Tester_Name");
            entity.Property(e => e.TransactBy)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Transact_By");
        });

        modelBuilder.Entity<TableMasterStatusOrder>(entity =>
        {
            entity.ToTable("Table_Master_Status_Order");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("ID");
            entity.Property(e => e.StatusOrder)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Status_Order");
        });

        modelBuilder.Entity<TableMasterStatusResult>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_Table_Master_Result_Status");

            entity.ToTable("Table_Master_Status_Result");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("ID");
            entity.Property(e => e.ResultStatus)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Result_Status");
        });

        modelBuilder.Entity<TableMasterStatusRunning>(entity =>
        {
            entity.ToTable("Table_Master_Status_Running");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("ID");
            entity.Property(e => e.StatusRunning)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Status_Running");
        });

        modelBuilder.Entity<TableMasterUser>(entity =>
        {
            entity
                .HasKey(e => e.IdUser);
                entity.ToTable("Table_Master_User");

            entity.Property(e => e.IdUser)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("ID_User");
            entity.Property(e => e.Password)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.StationId).HasColumnName("Station_ID");
            entity.Property(e => e.UserLevel).HasColumnName("User_Level");
            entity.Property(e => e.Username)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<TableMasterUserLevel>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("Table_Master_User_Level");

            entity.Property(e => e.IdRoles)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("ID_Roles");
            entity.Property(e => e.UserLevel).HasColumnName("User_Level");
        });

        modelBuilder.Entity<TableMasterWorkplan>(entity =>
        {
            entity.HasKey(e => e.FlowId);

            entity.ToTable("Table_Master_Workplan");

            entity.Property(e => e.FlowId)
                .ValueGeneratedNever()
                .HasColumnName("Flow_ID");
            entity.Property(e => e.FlowDescription)
                .HasMaxLength(300)
                .IsUnicode(false)
                .HasColumnName("Flow_Description");
            entity.Property(e => e.FlowName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Flow_Name");
            entity.Property(e => e.LastModify)
                .HasColumnType("datetime")
                .HasColumnName("Last_Modify");
            entity.Property(e => e.LineId).HasColumnName("Line_ID");
            entity.Property(e => e.Process1).HasColumnName("Process_1");
            entity.Property(e => e.Process10).HasColumnName("Process_10");
            entity.Property(e => e.Process11).HasColumnName("Process_11");
            entity.Property(e => e.Process12).HasColumnName("Process_12");
            entity.Property(e => e.Process13).HasColumnName("Process_13");
            entity.Property(e => e.Process14).HasColumnName("Process_14");
            entity.Property(e => e.Process15).HasColumnName("Process_15");
            entity.Property(e => e.Process16).HasColumnName("Process_16");
            entity.Property(e => e.Process17).HasColumnName("Process_17");
            entity.Property(e => e.Process18).HasColumnName("Process_18");
            entity.Property(e => e.Process19).HasColumnName("Process_19");
            entity.Property(e => e.Process2).HasColumnName("Process_2");
            entity.Property(e => e.Process20).HasColumnName("Process_20");
            entity.Property(e => e.Process21).HasColumnName("Process_21");
            entity.Property(e => e.Process22).HasColumnName("Process_22");
            entity.Property(e => e.Process23).HasColumnName("Process_23");
            entity.Property(e => e.Process24).HasColumnName("Process_24");
            entity.Property(e => e.Process25).HasColumnName("Process_25");
            entity.Property(e => e.Process26).HasColumnName("Process_26");
            entity.Property(e => e.Process27).HasColumnName("Process_27");
            entity.Property(e => e.Process28).HasColumnName("Process_28");
            entity.Property(e => e.Process29).HasColumnName("Process_29");
            entity.Property(e => e.Process3).HasColumnName("Process_3");
            entity.Property(e => e.Process30).HasColumnName("Process_30");
            entity.Property(e => e.Process31).HasColumnName("Process_31");
            entity.Property(e => e.Process32).HasColumnName("Process_32");
            entity.Property(e => e.Process33).HasColumnName("Process_33");
            entity.Property(e => e.Process34).HasColumnName("Process_34");
            entity.Property(e => e.Process35).HasColumnName("Process_35");
            entity.Property(e => e.Process36).HasColumnName("Process_36");
            entity.Property(e => e.Process37).HasColumnName("Process_37");
            entity.Property(e => e.Process38).HasColumnName("Process_38");
            entity.Property(e => e.Process39).HasColumnName("Process_39");
            entity.Property(e => e.Process4).HasColumnName("Process_4");
            entity.Property(e => e.Process40).HasColumnName("Process_40");
            entity.Property(e => e.Process41).HasColumnName("Process_41");
            entity.Property(e => e.Process42).HasColumnName("Process_42");
            entity.Property(e => e.Process43).HasColumnName("Process_43");
            entity.Property(e => e.Process44).HasColumnName("Process_44");
            entity.Property(e => e.Process45).HasColumnName("Process_45");
            entity.Property(e => e.Process46).HasColumnName("Process_46");
            entity.Property(e => e.Process47).HasColumnName("Process_47");
            entity.Property(e => e.Process48).HasColumnName("Process_48");
            entity.Property(e => e.Process49).HasColumnName("Process_49");
            entity.Property(e => e.Process5).HasColumnName("Process_5");
            entity.Property(e => e.Process50).HasColumnName("Process_50");
            entity.Property(e => e.Process6).HasColumnName("Process_6");
            entity.Property(e => e.Process7).HasColumnName("Process_7");
            entity.Property(e => e.Process8).HasColumnName("Process_8");
            entity.Property(e => e.Process9).HasColumnName("Process_9");
            entity.Property(e => e.ProcessQty).HasColumnName("Process_Qty");
            entity.Property(e => e.RefferenceName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Refference_Name");
            entity.Property(e => e.RevisionNumber)
                .HasDefaultValueSql("((1))")
                .HasColumnName("Revision_Number");
            entity.Property(e => e.TransactBy)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Transact_By");
            entity.Property(e => e.ValidStatus)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Valid_Status");
        });

        modelBuilder.Entity<TableRunTraceability>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("Table_Run_Traceability");

            entity.Property(e => e.BatchId)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Batch_ID");
            entity.Property(e => e.CavityNumber)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Cavity_Number");
            entity.Property(e => e.FullRefference)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasDefaultValueSql("('')");
            entity.Property(e => e.RefferenceName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Refference_Name");
            entity.Property(e => e.SerialNumber)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Serial_Number");
            entity.Property(e => e.StationId).HasColumnName("Station_ID");
            entity.Property(e => e.StationName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Station_Name");
            entity.Property(e => e.StationSuffix).HasColumnName("Station_Suffix");
            entity.Property(e => e.StatusResult)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Status_Result");
            entity.Property(e => e.StatusRunning)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Status_Running");
            entity.Property(e => e.TimeIn)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("Time_In");
            entity.Property(e => e.TimeOut)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("Time_Out");
            entity.Property(e => e.TransactBy)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Transact_By");
            entity.Property(e => e.UserId)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("User_ID");
            entity.Property(e => e.WorkOrder)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Work_Order");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.ToTable("User");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Password)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("password");
            entity.Property(e => e.Role)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("role");
            entity.Property(e => e.Username)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("username");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
